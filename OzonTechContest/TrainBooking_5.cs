namespace OzonTechContest
{
    /// <summary>
    /// Задача "Система продажи билетов на поезда"
    /// </summary>
    /// <remarks>
    /// Есть n купе, пронумерованных от 1 до n, в каждом купе два места, поступают запросы трех видов:
    /// Занять место, если оно еще не занято.
    /// Освободить место, если оно занято.
    /// Занять купе с наименьшим номером, в котором все места свободны.
    /// Если в купе освободили все места — то оно также считается свободным.
    /// </remarks>
    public static class TrainBooking
    {
        /// <summary>
        /// Обработка бронирований
        /// </summary>
        public static void ProcessBookings()
        {
            var sourceStr = Console.ReadLine() ?? string.Empty;
            var sourceArrayStr = sourceStr.Split(new[] { ' ' });

            if (sourceArrayStr.Length != 2)
            {
                throw new ArgumentException("Некорректная инициализация системы бронирования");
            }

            var placesCount = int.Parse(sourceArrayStr[0]);
            var queries = int.Parse(sourceArrayStr[1]);
            var places = new bool[placesCount];      // Список мест в вагоне, их флаг занятости

            var coupes = new SortedSet<int>();      // Список доступных купе
            for (var i = 0; i < placesCount; i += 2)
            {
                coupes.Add(i);
            }

            for (var i = 0; i < queries; ++i)
            {
                var queryStr = Console.ReadLine() ?? string.Empty;
                var queryArray = queryStr.Split(new[] { ' ' });

                switch (queryArray.Length)
                {
                    //// Забронировать купе
                    case 1:
                        if (coupes.Count == 0)
                        {
                            Console.WriteLine("FAIL");
                        }
                        else
                        {
                            var coupeIndex = coupes.First();
                            places[coupeIndex] = true;
                            places[coupeIndex + 1] = true;

                            coupes.Remove(coupeIndex);
                        }

                        break;

                    //// Бронь отдельного места
                    case 2:
                        var queryType = queryArray[0];
                        var placeNumber = int.Parse(queryArray[1]);

                        switch (queryType)
                        {
                            //// Забронировать место
                            case "1":
                                if (places[placeNumber])
                                {
                                    Console.WriteLine("FAIL");
                                }
                                else
                                {
                                    places[placeNumber] = true;
                                    var coupe = placeNumber % 2 == 0 ? placeNumber : placeNumber - 1;

                                    coupes.Remove(coupe);
                                    Console.WriteLine("SUCCESS");
                                }
                                break;
                            //// Снять бронь с места
                            default:
                                if (places[placeNumber])
                                {
                                    places[placeNumber] = false;
                                    var coupe = placeNumber % 2 == 0 ? placeNumber : placeNumber - 1;

                                    coupes.Add(coupe);
                                    Console.WriteLine("SUCCESS");
                                }
                                else
                                {
                                    Console.WriteLine("FAIL");
                                }
                                break;
                        }

                        break;
                    default:
                        throw new ArgumentException("Некорректный запрос бронирования.");
                }
            }
        }
    }
}