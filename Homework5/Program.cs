using System;

namespace Homework4
{
    /// <summary>
    /// Структура, представляющая студента
    /// </summary>
    struct Student
    {
        public string firstName; ///< Имя студента
        public string lastName; ///< Фамилия студента
        public DateTime birthYear; ///< Год рождения студента
        public string examName; ///< Название экзамена
        public int score; ///< Оценка за экзамен
    }
    /// <summary>
    /// Структура, представляющая бабушку
    /// </summary>
    struct Babushka
    {
        public string name; ///< Имя бабушки
        public int age; ///< Возраст бабушки
        public List<string> deseases; ///< Список болезней бабушки
        public List<string> medicines; ///< Список лекарств от болезней
        public string status; ///< Статус бабушки (например, "жива", "в больнице")
    }

    /// <summary>
    /// Структура, представляющая больницу
    /// </summary>
    struct Hospital
    {
        public string name; ///< Название больницы
        public List<string> deseases; ///< Список болезней, которые лечит больница
        public int capacity; ///< Вместимость больницы
        public int waitList; ///< Список ожидающих пациентов
    }
    class Program
    {
        /// <summary>
        /// Пример 1: Перемешивание списка файлов в каталоге
        /// </summary>
        static void Ex1()
        {
            string imageDirectory = "image";
            if (!Directory.Exists(imageDirectory))
            {
                Console.WriteLine($"Каталог {imageDirectory} не найден.");
                return;
            }

            var files = Directory.GetFiles(imageDirectory);

            var fileList = new List<string>();
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                fileList.Add(fileName);
            }

            Console.WriteLine("Исходный список файлов:");
            for (int i = 0; i < fileList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {fileList[i]}");
            }

            var random = new Random();
            fileList = fileList.OrderBy(_ => random.Next()).ToList();

            Console.WriteLine("\nПеремешанный список файлов:");
            for (int i = 0; i < fileList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {fileList[i]}");
            }
        }
        /// <summary>
        /// Пример 2: Работа со студентами
        /// </summary>
        static void Ex2()
        {
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            string filePath = "students.txt";

            LoadStudentsFromFile(filePath, students);

            char choice;
            do
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Новый студент");
                Console.WriteLine("2. Удалить");
                Console.WriteLine("3. Сортировать");
                Console.WriteLine("4. Вывести всех студентов");
                Console.WriteLine("5. Выход");
                Console.Write("Ваш выбор: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        if (!AddStudent(students))
                        {
                            Console.WriteLine("Ученик не добавлен");
                        }
                        break;
                    case '2':
                        RemoveStudent(students);
                        break;
                    case '3':
                        SortStudents(students);
                        break;
                    case '4':
                        PrintAllStudents(students);
                        break;
                    case '5':
                        SaveStudentsToFile(filePath, students);
                        Console.WriteLine("Изменения сохранены. До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            } while (choice != '5');
        }
        /// <summary>
        /// Загрузка данных о студентах из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="students">Словарь студентов</param>
        static void LoadStudentsFromFile(string filePath, Dictionary<string, Student> students)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        var student = new Student
                        {
                            firstName = parts[0],
                            lastName = parts[1],
                            birthYear = DateTime.Parse(parts[2]),
                            examName = parts[3],
                            score = int.Parse(parts[4])
                        };
                        string key = $"{student.firstName} {student.lastName}";
                        students[key] = student;
                    }
                }
                Console.WriteLine("Данные успешно загружены из файла.");
            }
            else
            {
                Console.WriteLine("Файл не найден. Начинаем с пустого списка.");
            }
        }
        /// <summary>
        /// Сохранение данных о студентах в файл
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="students">Словарь студентов</param>
        static void SaveStudentsToFile(string filePath, Dictionary<string, Student> students)
        {
            var lines = students.Values.Select(s => $"{s.firstName},{s.lastName},{s.birthYear},{s.examName},{s.score}");
            File.WriteAllLines(filePath, lines);
        }
        /// <summary>
        /// Добавление нового студента
        /// </summary>
        /// <param name="students">Словарь студентов</param>
        /// <returns>True, если студент добавлен успешно, иначе False</returns>
        static bool AddStudent(Dictionary<string, Student> students)
        {
            try
            {
                Console.Write("Введите имя: ");
                string firstName = Console.ReadLine();
                Console.Write("Введите фамилию: ");
                string lastName = Console.ReadLine();
                Console.Write("Введите год рождения (yyyy): ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    throw new FormatException("Неправильный ввод года рождения");
                }
                DateTime birthYear = new DateTime(year, 1, 1);

                Console.Write("Введите экзамен: ");
                string examName = Console.ReadLine();
                Console.Write("Введите баллы: ");
                if (!int.TryParse(Console.ReadLine(), out int score))
                {
                    throw new FormatException("Неправильный ввод количества баллов");
                }
                var student = new Student
                {
                    firstName = firstName,
                    lastName = lastName,
                    birthYear = birthYear,
                    examName = examName,
                    score = score
                };
                students[$"{firstName} {lastName}"] = student;
                Console.WriteLine("Студент добавлен.");
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Удаление студента из списка
        /// </summary>
        /// <param name="students">Словарь студентов</param>
        static void RemoveStudent(Dictionary<string, Student> students)
        {
            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            string key = $"{firstName} {lastName}";

            if (students.Remove(key))
            {
                Console.WriteLine("Студент удалён.");
            }
            else
            {
                Console.WriteLine("Студент не найден.");
            }
        }
        /// <summary>
        /// Сортировка студентов по баллам
        /// </summary>
        /// <param name="students">Словарь студентов</param>
        static void SortStudents(Dictionary<string, Student> students)
        {
            var sorted = students.Values.OrderBy(s => s.score).ToList();
            Console.WriteLine("\nСписок студентов после сортировки по баллам:");
            foreach (var student in sorted)
            {
                Console.WriteLine($"{student.lastName},{student.firstName},{student.birthYear.ToString("yyyy")},{student.examName},{student.score}");
            }
        }
        /// <summary>
        /// Вывод всех студентов
        /// </summary>
        /// <param name="students">Словарь студентов</param>
        static void PrintAllStudents(Dictionary<string, Student> students)
        {
            Console.WriteLine("\nСписок всех студентов:");
            foreach (var student in students.Values)
            {
                Console.WriteLine($"{student.lastName},{student.firstName},{student.birthYear.ToString("yyyy")},{student.examName},{student.score}");
            }
        }
        /// <summary>
        /// Пример 4: Поиск кратчайшего пути в графе
        /// </summary>
        static void Ex4()
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>
    {
        { "A", new List<string> { "B", "C" } },
        { "B", new List<string> { "A", "D", "E" } },
        { "C", new List<string> { "A", "F" } },
        { "D", new List<string> { "B" } },
        { "E", new List<string> { "B", "F" } },
        { "F", new List<string> { "C", "E" } }
    };

            List<string> path = FindShortestPath(graph, "A", "F");
            Console.WriteLine($"Кратчайший путь: {string.Join(" -> ", path)}");
        }
        /// <summary>
        /// Нахождение кратчайшего пути в графе
        /// </summary>
        /// <param name="graph">Граф, представленный в виде словаря</param>
        /// <param name="start">Стартовая вершина</param>
        /// <param name="end">Конечная вершина</param>
        /// <returns>Список вершин кратчайшего пути или null, если путь не найден</returns>

        static List<string> FindShortestPath(Dictionary<string, List<string>> graph, string start, string goal)
        {
            Queue<List<string>> queue = new Queue<List<string>>();
            queue.Enqueue(new List<string> { start });

            HashSet<string> visited = new HashSet<string>();

            while (queue.Count > 0)
            {
                List<string> path = queue.Dequeue();
                string node = path[^1];

                if (node == goal)
                    return path;

                if (!visited.Contains(node))
                {
                    visited.Add(node);
                    foreach (string neighbor in graph[node])
                    {
                        List<string> newPath = new List<string>(path) { neighbor };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return new List<string> { "Путь не найден" };
        }

        static void Main(string[] args)
        {

            Console.WriteLine("№1");
            /*Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List
            должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с
            картинками. Вывести в консоль перемешанные номера (изначальный List и полученный
            List). Перемешать любым способом.*/
            Ex1();
            Console.WriteLine("№2");
            /*Создать студента из вашей группы (фамилия, имя, год рождения, с каким экзаменом
            поступил, баллы). Создать словарь для студентов из вашей группы (10 человек).
            Необходимо прочитать информацию о студентах с файла. В консоли необходимо создать
            меню:
            a. Если пользователь вводит: Новый студент, то необходимо ввести
            информацию о новом студенте и добавить его в List
            b. Если пользователь вводит: Удалить, то по фамилии и имени удаляется
            студент
            c. Если пользователь вводит: Сортировать, то происходит сортировка по баллам
            (по возрастанию)*/
            Ex2();
            Console.WriteLine("№3");
            /*Создать бабулю. У бабули есть Имя, возраст, болезнь и лекарство от этой болезни,
            которое она принимает (болезней может быть у бабули несколько). Реализуйте список
            бабуль. Также есть больница (у больницы есть название, список болезней, которые они
            лечат и вместимость). Больниц также, как и бабуль несколько. Бабули по очереди
            поступают (реализовать ввод с клавиатуры) и дальше идут в больницу, в зависимости от
            заполненности больницы и списка болезней, которые лечатся в данной больнице,
            реализовать функционал, который будет распределять бабулю в нужную больницу. Если
            бабуля не имеет болезней, то она хочет только спросить - она может попасть в первую
            свободную клинику. Если бабулю ни одна из больниц не лечит, то бабуля остаётся на
            улице плакать. На экран выводить список всех бабуль, список всех больниц, болезни,
            которые там лечат и сколько бабуль в данный момент находится в больнице, также
            вывести процент заполненности больницы. P.S. Бабуля попадает в больницу, если там
            лечат более 50% ее болезней. Больницы реализовать в виде стека, бабуль в виде
            очереди.*/
            // Ex3();
            Console.WriteLine("№4");
            /*Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший путь.*/
            Ex4();



        }
    }
}





