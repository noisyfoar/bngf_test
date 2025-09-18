using System.Linq;
using System.Linq.Expressions;

namespace bngf_test_2
{
    internal class Program
    {

        class Stat
        {
            public Stat()
            {
                Name = "";
                Position = "";
                Increase = true;
                Value = 0;
            }

            public Stat(string name, string position, bool increase)
            {
                Name = name;
                Position = position;
                Increase = increase;
            }

            public string Name { get; set; }
            public dynamic? Value { get; set; }

            public string Position { get; set; }


            public bool Increase { get; set; }

            public void CompareAndSet(dynamic value, string name)
            {
                if ((value > Value) && (Increase))
                {
                    Value = value;
                    Name = name;
                }
                else if ((value < Value) && (!Increase))
                {
                    Value = value;
                    Name = name;
                }
            }
        }

        public static object ParseString(string value)
        {
            if (int.TryParse(value, out int intTemp))
                return intTemp;
            if (double.TryParse(value.Replace('.', ','), out double doubleTemp))
                return doubleTemp;
            return -1;
        }


        static void Main(string[] args)
        {


            string fileName = "Хоккеисты.csv";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] input = File.ReadAllLines(filePath);
            Dictionary<string, string> KeyByPosition = new Dictionary<string, string>()
            {
                { "ВР", "%ОБ" },
                { "ЗАЩ", "+/-" },
                { "НАП", "Ш" },
            };

            List<Stat> stats = [];
            int cursor = 0;
            for (int j = 0; j < 3; j++)
            {
                string[] columns = input[cursor].Split(',');

                cursor++;

                Stat stat = new();
                string[] player = input[cursor].Split(',');
                for (; (!player.All(string.IsNullOrEmpty)) && (cursor < input.Length - 1); cursor++, player = input[cursor].Split(','))
                {
                    String name = player[0];
                    String position = player[1];
                    int index = Array.IndexOf(columns, KeyByPosition[position]);
                    dynamic value = ParseString(player[index]);
                    stat.CompareAndSet(value, name);
                    stat.Position = position;


                }
                stats.Add(stat);
                cursor++;
            }
            foreach (Stat elem in stats)
            {
                Console.WriteLine($"{elem.Position},{elem.Name}");
            }

        }
    }
}
