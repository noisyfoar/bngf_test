namespace bngf_test_3
{
    internal class Program
    {
        /* 
           Задача на заливку.
        Реализован методом заливания строк. Для больших данных может быть переполнения стека.
         */
        class SquareField
        {
            public int[,] field;
            public int nextColor;
            int size;

            public SquareField(int size)
            {
                Random random = new Random();
                field = new int[size, size];
                nextColor = 2;
                this.size = size;
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        field[i, j] = random.NextDouble() > 0.3 ? 1 : 0;
            }

            public void FillFieldAndShowSteps()
            {
                var cursor = GetFirstEntryOfOne();
                while (cursor != (-1, -1))
                {
                    ColorArea(cursor.indexOfRow, cursor.indexOfColumn, nextColor);

                    Console.WriteLine($"Выделение {nextColor - 1} группы:");
                    PrintTable();

                    cursor = GetFirstEntryOfOne();
                    nextColor++;
                }
            }

            public void ColorArea(int startRow, int startCol, int newColor)
            {
                Stack<(int row, int col)> stack = new Stack<(int, int)>();
                stack.Push((startRow, startCol));

                while (stack.Count > 0)
                {
                    var current = stack.Pop();
                    int row = current.row;
                    int col = current.col;


                    int leftBorder = col;
                    while (leftBorder >= 0 && field[row, leftBorder] == 1)
                        leftBorder--;
                    leftBorder++;


                    int rightBorder = col;
                    while (rightBorder < size && field[row, rightBorder] == 1)
                        rightBorder++;
                    rightBorder--;


                    for (int i = leftBorder; i <= rightBorder; i++)
                        field[row, i] = newColor;


                    CheckRowForNewPoints(row - 1, leftBorder, rightBorder, newColor, stack);
                    CheckRowForNewPoints(row + 1, leftBorder, rightBorder, newColor, stack);
                }
            }

            private void CheckRowForNewPoints(int row, int left, int right, int newColor, Stack<(int, int)> stack)
            {
                if (row < 0 || row >= size)
                    return;

                for (int cursor = left; cursor <= right; cursor++)
                    if (field[row, cursor] == 1)
                        stack.Push((row, cursor));
            }

            public (int indexOfRow, int indexOfColumn) GetFirstEntryOfOne()
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (field[i, j] == 1)
                            return (i, j);
                return (-1, -1);
            }

            public void PrintTable()
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write($"{field[i, j]} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер поля: ");
            int size = Convert.ToInt32(Console.ReadLine());
            SquareField field = new SquareField(size);
            Console.WriteLine("Исходное поле:");
            field.PrintTable();
            field.FillFieldAndShowSteps();
        }
    }
}