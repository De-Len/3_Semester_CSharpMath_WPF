using System.IO;
using System.Text.Json;

namespace Semestr_3_Lab_0_WPF.Models
{
    class SecondControlModel
    {
        public static List<Flower> GenerateRandomFlowers(int count)
        {
            var random = new Random();
            var flowers = new List<Flower>();

            for (int index = 0; index < count; ++index)
            {
                var flower = new Flower
                {
                    FlowerName = FlowersData.FlowerNames[random.Next(FlowersData.FlowerNames.Length)],
                    FloweringMonth = FlowersData.FloweringMonths[random.Next(FlowersData.FloweringMonths.Length)],
                    FlowerSize = random.Next(5, 21)
                };
                flowers.Add(flower);
            }

            return flowers;
        }
    }
    public class Flower
    {
        public string FlowerName { get; set; }
        public string FloweringMonth { get; set; }
        public int FlowerSize { get; set; }
    }
    internal static class DataSerializer<T>
    {
        public static void Serialize(T data, string filepath)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filepath, jsonString);
        }
    }
    internal static class DataDeserializer<T>
    {
        public static T Deserialize(string filepath)
        {
            string jsonFromFile = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<T>(jsonFromFile);
        }
    }


}
