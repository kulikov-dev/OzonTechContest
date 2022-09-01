using System.Text.RegularExpressions;

namespace OzonTechContest
{
    /// <summary>
    /// Задача "Подсистема регистрации"
    /// </summary>
    /// <remarks>
    /// На вход подаются n логинов, для каждого логина вам нужно сказать, можно ли его зарегистрировать. Логин должен соответствовать следующим правилам:
    /// Логин — это последовательность из латинских букв в любом регистре, цифр и символов «_» или «-» (подчеркивание и дефис).
    /// Логин не должен начинаться с дефиса.
    /// Логин должен иметь длину от 2 до 24 символов.
    /// Логины, которые отличаются только регистром, считаются одинаковыми.
    /// </remarks>
    public sealed class LoginValidator_3
    {
        /// <summary>
        /// Список использованных логинов
        /// </summary>
        private readonly HashSet<string> logins = new();

        /// <summary>
        /// Метод обработки данных с консоли
        /// </summary>
        public void ProcessConsole()
        {
            //// Зачитываем количество входных данных
            var totalCountStr = Console.ReadLine();
            var totalCount = int.Parse(totalCountStr ?? "0");

            for (var i = 0; i < totalCount; ++i)
            {
                //// Проверяем каждый логин на валидность
                var login = Console.ReadLine();

                var result = IsValid(login);

                Console.WriteLine(result ? "YES" : "NO");
            }
        }

        /// <summary>
        /// Проверить, является ли логин валидным
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <returns> Флаг валидности </returns>
        public bool IsValid(string? login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            const string validLoginPattern = @"^[A-Za-z0-9_-]{2,23}$";

            var regex = new Regex(validLoginPattern);
            
            if (!regex.IsMatch(login))
            {
                return false;
            }

            if (!logins.Contains(login, StringComparer.OrdinalIgnoreCase))
            {
                logins.Add(login);
                return true;
            }

            return false;
        }
    }
}
