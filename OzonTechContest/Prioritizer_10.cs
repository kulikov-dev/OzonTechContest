namespace OzonTechContest
{
    /// <summary>
    /// Задача "Приоритезация задач"
    /// </summary>
    /// <remarks> В зависимости от важности задания необходимо выставить приоритет выполнения </remarks>
    internal class Prioritizer_10
    {
        /// <summary>
        /// Список поступивших задач
        /// </summary>
        private readonly List<TaskInfo> _tasks;

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public Prioritizer_10()
        {
            var totalStr = Console.ReadLine();
            var total = int.Parse(totalStr ?? "0");

            _tasks = new List<TaskInfo>();
            var importanceStr = Console.ReadLine() ?? string.Empty;
            var splittedImportances = importanceStr.Split(new[] { ' ' });

            if (splittedImportances.Length != total)
            {
                throw new ArgumentException("Некорректные входные данные: важность задач.");
            }

            for (var i = 0; i < total; ++i)
            {
                var importance = int.Parse(splittedImportances[i]);

                _tasks.Add(new TaskInfo(importance, i));
            }

            Prioritize();
        }

        /// <summary>
        /// Выполнить приоритизацию
        /// </summary>
        private void Prioritize()
        {
            if (_tasks.Count == 0)
            {
                return;
            }

            var currentNice = 1;

            var orderedTasks = _tasks.OrderByDescending(task => task.Importance).ToList();
            var currentMax = orderedTasks.First().Importance;

            foreach (var task in orderedTasks)
            {
                if (task.Importance != currentMax && task.Importance != currentMax - 1)
                {
                    ++currentNice;

                    currentMax = task.Importance;
                }

                task.Priority = currentNice;
            }

            Console.WriteLine(string.Join(' ', orderedTasks.OrderBy(x => x.Order).Select(x => x.Priority)));
        }

        /// <summary>
        /// Структура для хранения задачи
        /// </summary>
        private class TaskInfo
        {
            /// <summary>
            /// Важность задания
            /// </summary>
            public readonly int Importance;

            /// <summary>
            /// Приоритет выполнения
            /// </summary>
            public int Priority = -1;

            /// <summary>
            /// Порядковый номер задачи
            /// </summary>
            public readonly int Order;

            /// <summary>
            /// Конструктор с параметрами
            /// </summary>
            /// <param name="importance"> Важность </param>
            /// <param name="order"> Порядок </param>
            public TaskInfo(int importance, int order)
            {
                Importance = importance;
                Order = order;
            }
        }
    }
}
