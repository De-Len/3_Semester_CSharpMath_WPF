using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using System.CodeDom;
using System.Windows;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.LinearEquationsSystemMethodsPage.Windows
{
    internal class LinearEquationsSystemMethodsGoogleSheetWindowModel
    {
        public List<List<double>> GetDataFromGoogleSheet(string SpreadsheetId, string SheetName, string PathToCredentialsJson)
        {
            try
            {
                // Загрузка учетных данных из JSON-файла
                GoogleCredential credential;
                using (var stream = new FileStream(PathToCredentialsJson, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
                }

                // Создание сервиса Sheets API
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google Sheets API C# Quickstart",
                });

                // Получение метаданных таблицы
                SpreadsheetsResource.GetRequest metadataRequest = service.Spreadsheets.Get(SpreadsheetId);
                metadataRequest.Ranges = null;
                metadataRequest.IncludeGridData = false;

                Spreadsheet spreadsheet = metadataRequest.Execute();

                var sheet = spreadsheet.Sheets.FirstOrDefault(s => s.Properties.Title == SheetName);

                if (sheet == null)
                {
                    throw new Exception($"Лист с именем '{SheetName}' не найден.");
                    return null;
                }

                int? rowCount = sheet.Properties.GridProperties.RowCount;
                int? columnCount = sheet.Properties.GridProperties.ColumnCount;

                if (rowCount.HasValue && columnCount.HasValue)
                {
                    string GetColumnName(int index)
                    {
                        string columnName = "";
                        while (index > 0)
                        {
                            int modulo = (index - 1) % 26;
                            columnName = Convert.ToChar(65 + modulo) + columnName;
                            index = (index - modulo) / 26;
                        }
                        return columnName;
                    }

                    string lastColumn = GetColumnName(columnCount.Value);
                    string lastRow = rowCount.Value.ToString();

                    string dynamicRange = $"{SheetName}!A1:{lastColumn}{lastRow}";
                    Console.WriteLine($"Динамически определённый диапазон: {dynamicRange}");

                    // Запрос на чтение данных из таблицы
                    SpreadsheetsResource.ValuesResource.GetRequest dataRequest =
                            service.Spreadsheets.Values.Get(SpreadsheetId, dynamicRange);

                    // Получение ответа
                    ValueRange dataResponse = dataRequest.Execute();
                    IList<IList<Object>> dataValues = dataResponse.Values;

                    if (dataValues != null && dataValues.Count > 0)
                    {
                        return ConvertToDoubleList(dataValues);
                    }
                    else
                    {
                        throw new Exception("Нет данных для отображения.");
                    }
                }
                else
                {
                    throw new Exception("Не удалось определить количество строк или столбцов.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }

        public static List<List<double>> ConvertToDoubleList(IList<IList<Object>> listDataFromGoogleSheet)
        {
            List<List<double>> doubleList = new List<List<double>>();

            foreach (IList<Object> row in listDataFromGoogleSheet)
            {
                List<double> doubleRow = new List<double>();

                foreach (Object cell in row)
                {
                    double value;
                    // Попробуйте преобразовать объекты в double
                    if (double.TryParse(cell.ToString(), out value))
                    {
                        doubleRow.Add(value);
                    }
                    else
                    {
                        // В случае неудачного преобразования добавьте 0 или обработайте это по-другому
                        doubleRow.Add(0); // или можно вызывать ошибку/предупреждение
                    }
                }

                doubleList.Add(doubleRow);
            }

            return doubleList;
        }
    }
}
