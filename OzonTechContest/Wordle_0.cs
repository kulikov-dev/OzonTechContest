namespace OzonTechContest
{
    /// <summary>
    /// Задача "Угаданное слово"
    /// </summary>
    /// <remarks>
    /// Даны два слова: исходное слово и угаданное слово. Требуется их сравнить и вывести результат сравнения, указав:
    /// G - если символ в угаданном слове соответствует исходному
    /// Y - если такой символ есть, но стоит не на своем месте (учесть дубликаты символов)
    /// . - если такого символа нет, либо он уже использован
    /// </remarks>
    public static class Wordle_0
    {
        /// <summary>
        /// Получить результат сопоставление исходного и угаданного слов
        /// </summary>
        /// <param name="sourceStr"> Исходное слово </param>
        /// <param name="guess"> Угаданное слово </param>
        /// <returns> Результат сопоставления </returns>
        public static string CompareWords(string sourceStr, string guess)
        {
            var result = new int[sourceStr.Length];
            var dict = new Dictionary<char, int>();         // Словарь сопоставления символ - частота встречаемости
                                                            
            for (var i = 0; i < sourceStr.Length; ++i)
            {
                if (!dict.ContainsKey(sourceStr[i]))
                {
                    dict.Add(sourceStr[i], 0);
                }

                if (sourceStr[i] == guess[i])
                {
                    result[i] = 'G';
                }
                else
                {
                    dict[sourceStr[i]]++;
                }
            }

            //// Анализ несоответствующих символов
            for (var i = 0; i < guess.Length; ++i)
            {
                if (result[i] != 0)
                {
                    continue;
                }

                if (!dict.ContainsKey(guess[i]))
                {
                    result[i] = '.';
                }
                else
                {
                    result[i] = dict[guess[i]] > 0 ? 'Y' : '.';
                    --dict[guess[i]];
                }
            }

            return new string(result.Select(ch => (char)ch).ToArray());
        }
    }
}