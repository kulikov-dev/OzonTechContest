namespace OzonTechContest
{
    /// <summary>
    /// Задача "Адресная книга"
    /// </summary>
    /// <remarks>
    /// Дается журнал звонков — набор записей (имя звонившего, телефон звонившего). Записи даны в хронологическом порядке от наиболее ранней к самой последней.
    /// Требуется восстановить для каждого звонившего 5 последних его номеров телефона.
    /// Записи могут встречаться несколько раз, то есть возможна ситуация, когда одна пара(имя звонившего, телефон звонившего) встречается два и более раза во входных данных.
    /// </remarks>
    public sealed class AddressBook_4
    {
        /// <summary>
        /// Метод обработки данных с консоли
        /// </summary>
        /// <exception cref="ArgumentException"> Ошибка входных данных </exception>
        public void ProcessConsole()
        {
            var callRecords = new List<CallRecord>();
            var totalPhonesStr = Console.ReadLine();
            var totalPhones = int.Parse(totalPhonesStr ?? "0");

            //// Зачитываем все записи и формируем список звонков
            for (var i = 0; i < totalPhones; ++i)
            {
                var itemStr = Console.ReadLine() ?? string.Empty;
                var itemArrayStr = itemStr.Split(new[] { ' ' });

                if (itemArrayStr.Length != 2)
                {
                    throw new ArgumentException($"Некорректная запись в звонках: {itemStr}");
                }

                callRecords.Add(new CallRecord(itemArrayStr[0], itemArrayStr[1]));
            }

            var addressBook = GetValidAddressBook(callRecords);

            PrintBook(addressBook);
        }

        /// <summary>
        /// Получить актуальную телефонную книгу
        /// </summary>
        /// <param name="callRecords"> Список звонков </param>
        /// <returns> Телефонная книга </returns>
        public static SortedDictionary<string, Queue<string>> GetValidAddressBook(List<CallRecord> callRecords)
        {
            //// Словарь для хранения звонивших в лексикографическом порядке
            /// Key - имя звонившего, Value - список номеров, с которых был звонок
            var result = new SortedDictionary<string, Queue<string>>();

            foreach (var record in callRecords)
            {
                if (!result.ContainsKey(record.Name))
                {
                    result.Add(record.Name, new Queue<string>());
                }

                var element = result[record.Name];

                element.Enqueue(record.Phone);
                if (element.Count > 5)
                {
                    //// Проверка на количество, согласно условию в адресной книге должно быть не больше 5 номеров одного абонента
                    element.Dequeue();
                }
            }

            return result;
        }

        /// <summary>
        /// Вывод на экран
        /// </summary>
        /// <param name="addressBook"> Телефонная книга </param>
        private static void PrintBook(SortedDictionary<string, Queue<string>> addressBook)
        {
            foreach (var item in addressBook)
            {
                var phones = item.Value.ToList();

                phones.Reverse();           //// Так как у нас очередь, а требуется вывести в обратном порядке - разворачиаем список телефонов
                Console.WriteLine($"{item.Key}: {phones.Count} {string.Join(' ', phones)}");
            }
        }

        /// <summary>
        /// Информация о звонившем
        /// </summary>
        public struct CallRecord
        {
            /// <summary>
            /// Имя
            /// </summary>
            public string Name;

            /// <summary>
            /// Телефон
            /// </summary>
            public string Phone;

            /// <summary>
            /// Конструктор с параметрами
            /// </summary>
            /// <param name="name"> Имя </param>
            /// <param name="phone"> Телефон </param>
            public CallRecord(string name, string phone)
            {
                Name = name;
                Phone = phone;
            }
        }
    }
}