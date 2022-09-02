namespace OzonTechContest
{
    /// <summary>
    /// Задача "Электронная таблица"
    /// </summary>
    /// <remarks>
    /// Задана прямоугольная таблица n * m из чисел. Требуется обработать k запросов: примените стабильную сортировку по неубыванию к столбцу i.
    /// Требуется вывести таблицу после обработки всех запросов.
    /// </remarks>
    internal class TableRowsSorter_2
    {
        private List<List<int>>? _table;

        /// <summary>
        /// Метод обработки данных с консоли
        /// </summary>
        public void ProcessConsole()
        {
            _table = new List<List<int>>();
            var nStr = Console.ReadLine();
            var mStr = Console.ReadLine();
            var n = int.Parse(nStr ?? "0");
            var m = int.Parse(mStr ?? "0");

            //// Зачитываем исходную таблицу
            for (var i = 0; i < n; ++i)
            {
                var currentLine = new List<int>();
                var lineStr = Console.ReadLine() ?? string.Empty;
                var lineArray = lineStr.Split(new[] { ' ' });

                if (lineArray.Length != m)
                {
                    throw new ArgumentException("Некорректные данные таблицы");
                }

                for (var j = 0; j < m; ++j)
                {
                    currentLine.Add(int.Parse(lineArray[j]));
                }

                _table.Add(currentLine);
            }

            ProcessSorts();

            ShowTable();
        }

        /// <summary>
        /// Обработка сортировок
        /// </summary>
        private void ProcessSorts()
        {
            //// Затем входные данные содержат строку с один целым числом k(1≤k≤30) — количество кликов.
            /// Следующая строка содержит k целых чисел c1,c2,…,ck(1≤ci≤m) — номера столбцов, по которым были осуществлены клики. Клики даны в порядке их совершения.

            var kStr = Console.ReadLine();
            var k = int.Parse(kStr ?? "0");
            var clicks = new Dictionary<int, int>();         // Key - индекс столбца, value- последний актуальный клик
            var clicksStr = Console.ReadLine() ?? string.Empty;
            var clicsArrayStr = clicksStr.Split(new[] { ' ' });

            if (clicsArrayStr.Length != k)
            {
                throw new ArgumentException("Некорректные входные данные по кликам.");
            }

            for (var i = 0; i < k; ++i)
            {
                //// Использование словаря позволяет не производить несколько раз сортировку
                var rowIndex = int.Parse(clicsArrayStr[i]);
                if (!clicks.ContainsKey(rowIndex))
                {
                    clicks.Add(rowIndex, i);
                }
                else
                {
                    clicks[rowIndex] = 1;
                }
            }

            var rowIndexesToSort = clicks.OrderBy(x => x.Value).Select(x => x.Key);
            foreach (var rowIndex in rowIndexesToSort)
            {
                //// Используем стабильную сортировку
                _table = _table.OrderBy(x => x[rowIndex]).ToList();
            }
        }

        /// <summary>
        /// Вывод таблицы
        /// </summary>
        public void ShowTable()
        {
            if (_table == null)
            {
                return;
            }

            foreach (var row in _table)
            {
                Console.WriteLine(string.Join(' ', row));
            }
        }
    }
}