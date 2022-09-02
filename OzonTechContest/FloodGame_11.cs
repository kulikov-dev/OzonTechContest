namespace OzonTechContest
{
    /// <summary>
    /// Задача "Игра - наводнение"
    /// </summary>
    /// <remarks>
    /// Есть игровое поле (аналог тетриса), на котором '*' обозначены препятствия.
    /// Необходимо вывести игровое поле, получившееся в результате наводнения '~' (поток воды сверху-вниз)
    /// </remarks>
    public static class FloodGame_11
    {
        /// <summary>
        /// Обработка и решение игры
        /// </summary>
        /// <exception cref="ArgumentException"> Ошибка некорректных входных данных </exception>
        public static void Solve()
        {
            const char obstacleChar = '*';
            const char floodChar = '~';
            const char emptyFieldChar = '.';

            var sizeStr = Console.ReadLine() ?? string.Empty;
            var sizes = sizeStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (sizes.Length != 2)
            {
                throw new ArgumentException("Некорректные размеры игрового поля");
            }

            var n = int.Parse(sizes[0]);
            var m = int.Parse(sizes[1]);

            var blockCounter = Enumerable.Repeat(0, m).ToList();

            //// Считываем количество и положение препятствий на игровом поле
            for (var i = 0; i < n; ++i)
            {
                var line = Console.ReadLine() ?? string.Empty;

                if (line.Length != m)
                {
                    throw new ArgumentException("Некорректное игровое поле");
                }

                for (var j = 0; j < m; ++j)
                {
                    if (line[j] == obstacleChar)
                    {
                        blockCounter[j]++;
                    }
                }
            }

            var result = new char[n][];             // Результирующее игровое поле

            //// Затапливаем сверху вниз
            for (var i = n - 1; i >= 0; i--)
            {
                var startIndex = -1;
                result[n] = new char[m];

                for (var j = 0; j < m; ++j)
                {
                    if (blockCounter[j] > 0)
                    {
                        result[i][j] = obstacleChar;

                        if (startIndex == -1)
                        {
                            startIndex = j;
                        }
                        else
                        {
                            for (var k = startIndex + 1; k < j; ++k)
                            {
                                result[i][k] = floodChar;
                            }

                            startIndex = -1;
                        }
                    }
                    else
                    {
                        result[i][j] = emptyFieldChar;
                    }
                }
            }

            for (var i = 0; i < n; ++i)
            {
                Console.WriteLine(string.Join(string.Empty, result[i]));
            }
        }
    }
}