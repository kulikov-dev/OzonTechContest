namespace OzonTechContest
{
    /// <summary>
    /// Требуется реализовать проверку поля с сыгранной игрой крестики-нолики на корректность:
    /// Может быть только один победитель, нет лишних ходов, порядок ходов верен.
    /// </summary>
    public sealed class TicTacToe_8
    {
        /// <summary>
        /// Метод обработки данных с консоли
        /// </summary>
        public void ProcessConsole()
        {
            var totalX = 0;
            var totalO = 0;
            //// При чтении преобразуем поле символов в int поле
            /// Пустая ячейка (.) = 0, X = 1, O = 2
            var field = new int[3, 3];

            for (var i = 0; i < 3; ++i)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line) || line.Length != 3)
                {
                    throw new ArgumentException("Некорректное поле крестиков-ноликов");
                }

                //// Считываем входные строки с полем и подсчитываем общее количество крестиков и ноликов
                for (var j = 0; j < 3; ++j)
                {
                    switch (line[j])
                    {
                        case '.':

                            field[i, j] = 0;

                            break;
                        case 'X':
                            ++totalX;

                            field[i, j] = 1;

                            break;
                        default:
                            ++totalO;

                            field[i, j] = 2;

                            break;
                    }
                }
            }

            var isValid = Check(totalX, totalO, field);

            Console.WriteLine(isValid ? "YES" : "NO");
        }

        /// <summary>
        /// Проверить поле на корректность
        /// </summary>
        /// <param name="totalX"> Общее количество крестиков </param>
        /// <param name="totalO"> Общее количество ноликов </param>
        /// <param name="field"> Игровое поле </param>
        /// <returns> Поле корректно </returns>
        private static bool Check(int totalX, int totalO, int[,] field)
        {
            //// Проверяем корректность количества ходов
            if (totalX < totalO || totalX - totalO > 1)
            {
                return false;
            }

            var result = true;

            if (totalX > 2)
            {
                //// Проверяем каждого игрока на победу
                if (totalX == totalO)
                {
                    result = !CheckForWin(field, 1);
                }
                else if (totalX > totalO)
                {
                    result = !CheckForWin(field, 2);
                }
            }


            return result;
        }

        /// <summary>
        /// Проверка игрока на победу
        /// </summary>
        /// <param name="field"> Поле </param>
        /// <param name="checkValue"> Значение игрока </param>
        /// <returns> Игрок выиграл </returns>
        private static bool CheckForWin(int[,] field, int checkValue)
        {
            //// Прорабатываем различные варианты выигрышных комбинаций
            for (var i = 0; i < 3; ++i)
            {
                if (field[i, 0] == checkValue && field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2])
                {
                    return true;
                }

                if (field[0, i] == checkValue && field[0, i] == field[1, i] && field[1, i] == field[2, i])
                {
                    return true;
                }
            }

            return (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[2, 2] == checkValue) || (field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0] && field[2, 0] == checkValue);
        }
    }
}