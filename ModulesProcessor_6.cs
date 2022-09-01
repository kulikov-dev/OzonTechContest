namespace OzonTechContest
{
    /// <summary>
    /// Задача "Многомодульный проект"
    /// </summary>
    /// <remarks>
    /// Есть n модулей, для каждого известны зависимости, которые нужны для его установки.
    /// Есть m запросов — на каждой запрос нужно установить какой-то модуль, предварительно поставив все его зависимости, игнорируя уже установленные модули.
    /// </remarks>
    public sealed class ModulesProcessor_6
    {
        /// <summary>
        /// Метод обработки данных с консоли
        /// </summary>
        public void ProcessConsole()
        {
            Console.ReadLine();

            //// Загружаем список модулей и их зависимостей
            var dependencies = LoadDependencies();
            //// Загружаем список модулей для компиляции
            var modulesToCompile = LoadModulesToCompile();

            var result = new List<List<string>>();     // Список скомпилированных зависимых модулей для каждого moduleToCompile
            var compiledList = new HashSet<string>();  // Список скомпилированных модулей

            foreach (var module in modulesToCompile)
            {
                var subResult = new List<string>();

                //// Рекурсивно обрабатываем все зависимые модули, исключая уже скомпилированные
                Compile(module, dependencies, ref subResult, ref compiledList);
                result.Add(subResult);
            }

            foreach (var element in result)
            {
                //// Выводим количество и список скомпилированных (включая зависимые) модулей, для каждого модуля
                Console.WriteLine($"{element.Count} {string.Join(' ', element)}");
            }
        }

        /// <summary>
        /// Чтение списка модулей и их зависимостей
        /// </summary>
        /// <returns> Список модулей с зависимости </returns>
        /// <remarks> Key - модуль, Value - список модулей-зависимостей </remarks>
        private Dictionary<string, List<string>> LoadDependencies()
        {
            var dependencies = new Dictionary<string, List<string>>();
            var totalModules = int.Parse(Console.ReadLine() ?? "0");

            for (var i = 0; i < totalModules; ++i)
            {
                var line = Console.ReadLine() ?? string.Empty;
                var splittedLine = line.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (splittedLine.Length == 0)
                {
                    throw new ArgumentException("Некорректная входная строка при загрузке зависимостей.");
                }

                var module = splittedLine[0];

                if (!dependencies.ContainsKey(module))
                {
                    dependencies.Add(module, new List<string>());
                }

                for (var j = 1; j < splittedLine.Length; ++j)
                {
                    dependencies[module].Add(splittedLine[j]);
                }
            }

            return dependencies;
        }

        /// <summary>
        /// Чтение списка модулей для компиляции
        /// </summary>
        /// <returns> Модули для компиляции </returns>
        private List<string> LoadModulesToCompile()
        {
            var result = new List<string>();

            var compileModulesCount = int.Parse(Console.ReadLine() ?? "0");

            for (var i = 0; i < compileModulesCount; ++i)
            {
                var module = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(module))
                {
                    continue;
                }

                result.Add(module);
            }

            return result;
        }

        /// <summary>
        /// Рекурсивное определение зависимых сборок
        /// </summary>
        /// <param name="module"> Текущий модуль </param>
        /// <param name="dependencies"> Список зависимостей </param>
        /// <param name="result"> Компилированные модули </param>
        /// <param name="compiled"> Уже обработанные модули </param>
        private static void Compile(string module, Dictionary<string, List<string>> dependencies, ref List<string> result, ref HashSet<string> compiled)
        {
            if (compiled.Contains(module))
            {
                return;
            }

            compiled.Add(module);
            if (dependencies[module].Count == 0)
            {
                result.Add(module);
            }
            else
            {
                foreach (var subModule in dependencies[module])
                {
                    Compile(subModule, dependencies, ref result, ref compiled);
                }

                result.Add(module);
            }
        }
    }
}