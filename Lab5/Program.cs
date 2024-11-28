using System;

namespace Lab5
{
    class Program
    {
        /// <summary>
        /// Функция для чтения и печати содержимого файла.
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        static void Ex1(string fileName)
        {
            if (File.Exists(fileName))
            {
                string fileText = File.ReadAllText(fileName);
                Console.WriteLine(CountVowelsAndConsonants(fileText));
            }
            else
            {
                Console.WriteLine($"Файл {fileName} не найден.");
            }

        }

        /// <summary>
        /// Метод для подсчета гласных и согласных букв.
        /// </summary>
        /// <param name="characters">Массив символов</param>
        /// <returns>Кортеж с количеством гласных и согласных</returns>
        static (int vowels, int consonants) CountVowelsAndConsonants(string characters)
        {
            string vowelsList = "aeiouyаоуыиэеёюя";
            string consonantsList = "bcdfghjklmnpqrstvwxzбвгджзйклмнпрстфхцчшщ";

            int vowels = 0, consonants = 0;

            foreach (char c in characters.ToLower())
            {
                if (vowelsList.Contains(c))
                {
                    vowels++;
                }
                else if (consonantsList.Contains(c))
                {
                    consonants++;
                }
            }
            return (vowels, consonants);
        }
        /// <summary>
        /// Упражнение 6.2: умножение матриц.
        /// </summary>
        static void Ex2()
        {
            try
            {
                Console.WriteLine("Создание первой матрицы");
                if (!MatrixReading(out int[][] matrix1))
                {
                    throw new FormatException("Ошибка в создвнии матрицы");
                }
                if (!MatrixReading(out int[][] matrix2))
                {
                    throw new FormatException("Ошибка в создвнии матрицы");
                }

                if (matrix1[0].Length != matrix2.Length)
                {
                    throw new InvalidOperationException("Невозможно умножить матрицы: количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
                }

                int[][] product = MatrixMultiplication(matrix1, matrix2);
                Console.WriteLine("Результат умножения матриц:");
                MatrixWriting(product);

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }

        }


        static void MatrixWriting(int[][] matrix)
        {
            Console.WriteLine("Матрица:");
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join("\t", row));
            }
        }

        static int[][] MatrixMultiplication(int[][] matrix1, int[][] matrix2)
        {
            int rows = matrix1.Length;
            int cols = matrix2[0].Length;
            int common = matrix1[0].Length;

            int[][] product = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                product[i] = new int[cols];
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    for (int k = 0; k < common; k++)
                    {
                        product[i][j] += matrix1[i][k] * matrix2[k][j];
                    }
                }
            }

            return product;
        }
        static bool MatrixReading(out int[][] matrix)
        {
            try
            {
                Console.WriteLine("Введите количество строк матрицы: ");
                if (!int.TryParse(Console.ReadLine(), out int rows))
                {
                    throw new FormatException("Неправильный ввод количества строк");
                }
                Console.WriteLine("Введите количество столбцов матрицы: ");
                if (!int.TryParse(Console.ReadLine(), out int cols))
                {
                    throw new FormatException("Неправильный ввод количества столбцов");
                }
                matrix = new int[rows][];
                Console.WriteLine("Введите элементы матрицы:");

                for (int i = 0; i < rows; i++)
                {
                    matrix[i] = new int[cols];
                    for (int j = 0; j < cols; j++)
                    {
                        if (!int.TryParse(Console.ReadLine(), out matrix[i][j]))
                        {
                            throw new FormatException("Неправильный ввод элемента матрицы");
                        }
                    }
                }
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                matrix = null;
                return false;
            }
        }



        static void Ex3()
        {
            int months = 12;
            int daysInMonth = 30;
            Random random = new Random();
            int[,] temperature = new int[months, daysInMonth];

            for (int i = 0; i < months; i++)
            {
                for (int j = 0; j < daysInMonth; j++)
                {
                    temperature[i, j] = random.Next(-30, 31);
                }
            }

            double[] averageTemperatures = CalculateMonthlyAverages(temperature);

            Console.WriteLine("Средние температуры по месяцам (до сортировки):");
            for (int i = 0; i < months; i++)
            {
                Console.WriteLine($"Месяц {i + 1}: {averageTemperatures[i]:F2} °C");
            }

            Array.Sort(averageTemperatures);

            Console.WriteLine("\nСредние температуры по месяцам (после сортировки):");
            foreach (double avgTemp in averageTemperatures)
            {
                Console.WriteLine($"{avgTemp:F2} °C");
            }
        }

        /// <summary>
        /// Метод для вычисления средних температур по месяцам
        /// </summary>
        /// <param name="temperature">Двумерный массив температур</param>
        /// <returns>Массив средних температур</returns>
        static double[] CalculateMonthlyAverages(int[,] temperature)
        {
            int months = temperature.GetLength(0);
            int daysInMonth = temperature.GetLength(1);
            double[] averages = new double[months];

            for (int i = 0; i < months; i++)
            {
                int sum = 0;
                for (int j = 0; j < daysInMonth; j++)
                {
                    sum += temperature[i, j];
                }
                averages[i] = (double)sum / daysInMonth;
            }

            return averages;
        }

        static void Ex4(string fileName)
        {
            if (File.Exists(fileName))
            {
                string fileText = File.ReadAllText(fileName);
                List<char> characters = new List<char>(fileText);

                Console.WriteLine(CountVowelsAndConsonants(characters));
            }
            else
            {
                Console.WriteLine($"Файл {fileName} не найден.");
            }

        }
        static (int vowels, int consonants) CountVowelsAndConsonants(List<char> characters)
        {
            string vowelsList = "aeiouyаоуыиэеёюя";
            string consonantsList = "bcdfghjklmnpqrstvwxzбвгджзйклмнпрстфхцчшщ";

            int vowels = 0, consonants = 0;

            foreach (char c in characters)
            {
                if (vowelsList.Contains(c))
                {
                    vowels++;
                }
                else if (consonantsList.Contains(c))
                {
                    consonants++;
                }
            }
            return (vowels, consonants);
        }

        /// <summary>
        /// Упражнение 6.2: умножение матриц с использованием LinkedList.
        /// </summary>
        static void Ex5()
        {
            try
            {
                Console.WriteLine("Создание первой матрицы");
                if (!MatrixReading(out LinkedList<LinkedList<int>> matrix1))
                {
                    throw new FormatException("Ошибка в создвнии матрицы");
                }
                if (!MatrixReading(out LinkedList<LinkedList<int>> matrix2))
                {
                    throw new FormatException("Ошибка в создвнии матрицы");
                }
                {
                    if (matrix1.First.Value.Count != matrix2.Count)
                    {
                        throw new InvalidOperationException("Невозможно умножить матрицы: количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
                    }

                    LinkedList<LinkedList<int>> product = MatrixMultiplication(matrix1, matrix2);
                    Console.WriteLine("Результат умножения матриц:");
                    MatrixWriting(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод для чтения матрицы в формате LinkedList<LinkedList<int>>.
        /// </summary>
        static bool MatrixReading(out LinkedList<LinkedList<int>> matrix)
        {
            try
            {
                Console.WriteLine("Введите количество строк матрицы: ");
                if (!int.TryParse(Console.ReadLine(), out int rows))
                {
                    throw new FormatException("Неправильный ввод количества строк");
                }
                Console.WriteLine("Введите количество столбцов матрицы: ");
                if (!int.TryParse(Console.ReadLine(), out int cols))
                {
                    throw new FormatException("Неправильный ввод количества столбцов");
                }

                matrix = new LinkedList<LinkedList<int>>();

                Console.WriteLine("Введите элементы матрицы:");
                for (int i = 0; i < rows; i++)
                {
                    LinkedList<int> row = new LinkedList<int>();
                    for (int j = 0; j < cols; j++)
                    {
                        if (!int.TryParse(Console.ReadLine(), out int value))
                        {
                            throw new FormatException("Неправильный ввод элемента матрицы");
                        }
                        row.AddLast(value);
                    }
                    matrix.AddLast(row);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                matrix = null;
                return false;
            }
        }

        /// <summary>s
        /// Метод для умножения матриц в формате LinkedList<LinkedList<int>>.
        /// </summary>
        static LinkedList<LinkedList<int>> MatrixMultiplication(LinkedList<LinkedList<int>> matrix1, LinkedList<LinkedList<int>> matrix2)
        {
            int rows = matrix1.Count;
            int cols = matrix2.First.Value.Count;
            LinkedList<LinkedList<int>> product = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < rows; i++)
            {
                LinkedList<int> productRow = new LinkedList<int>();
                for (int j = 0; j < cols; j++)
                {
                    int sum = 0;

                    var rowIterator = matrix1.ElementAt(i).GetEnumerator();
                    var colIterator = GetColumn(matrix2, j).GetEnumerator();

                    while (rowIterator.MoveNext() && colIterator.MoveNext())
                    {
                        sum += rowIterator.Current * colIterator.Current;
                    }

                    productRow.AddLast(sum);
                }
                product.AddLast(productRow);
            }

            return product;
        }

        /// <summary>
        /// Метод для извлечения столбца из матрицы LinkedList<LinkedList<int>>.
        /// </summary>
        static LinkedList<int> GetColumn(LinkedList<LinkedList<int>> matrix, int columnIndex)
        {
            LinkedList<int> column = new LinkedList<int>();

            foreach (var row in matrix)
            {
                column.AddLast(row.ElementAt(columnIndex));
            }

            return column;
        }

        /// <summary>
        /// Метод для вывода матрицы в формате LinkedList<LinkedList<int>>.
        /// </summary>
        static void MatrixWriting(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Упражнение 6.3 с использованием Dictionary.
        /// </summary>
        static void Ex6()
        {
            Dictionary<string, int[]> temperatures = new Dictionary<string, int[]>()
            {
                { "Январь", GenerateRandomTemperatures(30) },
                { "Февраль", GenerateRandomTemperatures(30) },
                { "Март", GenerateRandomTemperatures(30) },
                { "Апрель", GenerateRandomTemperatures(30) },
                { "Май", GenerateRandomTemperatures(30) },
                { "Июнь", GenerateRandomTemperatures(30) },
                { "Июль", GenerateRandomTemperatures(30) },
                { "Август", GenerateRandomTemperatures(30) },
                { "Сентябрь", GenerateRandomTemperatures(30) },
                { "Октябрь", GenerateRandomTemperatures(30) },
                { "Ноябрь", GenerateRandomTemperatures(30) },
                { "Декабрь", GenerateRandomTemperatures(30) }
            };

            var monthlyAverages = temperatures.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Average()
            );

            Console.WriteLine("Средние температуры по месяцам (до сортировки):");
            foreach (var pair in monthlyAverages)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value:F2} °C");
            }

            Console.WriteLine("\nСредние температуры по месяцам (после сортировки):");
            foreach (var pair in monthlyAverages.OrderBy(kv => kv.Value))
            {
                Console.WriteLine($"{pair.Key}: {pair.Value:F2} °C");
            }

        }

        static int[] GenerateRandomTemperatures(int days)
        {
            Random random = new Random();
            return Enumerable.Range(0, days).Select(_ => random.Next(-30, 31)).ToArray();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Упражнение 6.1");
            /*Написать программу, которая вычисляет число гласных и согласных букв в
            файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла
            заносится в массив символов. Количество гласных и согласных букв определяется проходом
            по массиву. Предусмотреть метод, входным параметром которого является массив символов.
            Метод вычисляет количество гласных и согласных букв.*/
            if (args.Length != 0)
            {
                Ex1(args[0]); //example.txt
            }
            else
            {
                Console.WriteLine("Вы не ввели название файла");
            }

            Console.WriteLine("Упражнение 6.2");
            /*Написать программу, реализующую умножению двух матриц, заданных в
            виде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,
            метод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).*/
            Ex2();
            Console.WriteLine("Упражнение 6.3");
            /*Написать программу, вычисляющую среднюю температуру за год. Создать
            двумерный рандомный массив temperature[12, 30], в котором будет храниться температура
            для каждого дня месяца(предполагается, что в каждом месяце 30 дней).Сгенерировать
            значения температур случайным образом. Для каждого месяца распечатать среднюю
            температуру.Для этого написать метод, который по массиву temperature[12, 30] для каждого
            месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
            средних температур.Полученный массив средних температур отсортировать по
            возрастанию.*/
            Ex3();
            Console.WriteLine("Домашнее задание 6.1");
            /*Упражнение 6.1 выполнить с помощью коллекции List<T>.*/
            if (args.Length != 0)
            {
                Ex4(args[0]); //example.txt
            }
            else
            {
                Console.WriteLine("Вы не ввели название файла");
            }
            Console.WriteLine("Домашнее задание 6.2");
            /*Упражнение 6.2 выполнить с помощью коллекций LinkedList<LinkedList<T>>.*/
            Ex5();
            Console.WriteLine("Домашнее задание 6.3");
            /*Написать программу для упражнения 6.3, использовав класс
            Dictionary<TKey, TValue>.В качестве ключей выбрать строки – названия месяцев, а в
            качестве значений – массив значений температур по дням.*/
            Ex6();



        }
    }
}

