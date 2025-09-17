namespace bngf_test_1
{
    class Fruit(string NameOfFruit, int Weight, int Price)
    {
        public string NameOfFruit { get; set; } = NameOfFruit;
        public int Weight { get; set; } = Weight;
        public int Price { get; set; } = Price;

        public void Show() => Console.WriteLine($"{NameOfFruit};{Weight}");
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //  чтение файла, проверку на наличие файла и другого имени не предусмотрел, так как не посчитал важным
            string fileName = "shopping_list.txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] input = File.ReadAllLines(filePath);
            // вытаскиваем первую строку, где хранится вес
            int maxWeight = int.Parse(input[0]);

            // задаем список фруктов и список покупок
            List<Fruit> fruits = [];
            List<Fruit> purchases = [];


            // записываем данные из файла в список фруктов
            // подразумевается, что два фрукта одного вида не может быть в исходном файле
            for (int i = 1; i < input.Length; i++)
            {
                string[] temp = input[i].Split(';');
                if(temp.Length == 3)
                {
                    var fruit = new Fruit(
                        temp[0],
                        int.Parse(temp[1]),
                        int.Parse(temp[2]));
                    fruits.Add(fruit);
                }
            }

            // сортируем список фруктов по убыванию цены
            fruits = [.. fruits.OrderByDescending(f => f.Price)];


            //проходимся по списку фруктов, где в начале самые дорогие, чтобы сразу их добавить в список покупок
            foreach(var fruit in fruits)
            {
                // берем минимум из двух:
                // если больше оставшийся вес, то кладем все фрукты этого типа
                // если больше фруктов, то кладем сколько может уместиться
                int temp = Math.Min(maxWeight, fruit.Weight);
                fruit.Weight = temp;
                // занимаем место в закупке
                maxWeight -= temp;
                purchases.Add(fruit);
                // если места больше нет, то выходим из списка
                if (maxWeight <= 0)
                {
                    break;
                }
            }
            foreach(var fruit in purchases)
            {
                fruit.Show();
            }
            Console.WriteLine("Для выхода нажмите любую кнопку:");
            Console.ReadKey();
        }
        
    }
}
