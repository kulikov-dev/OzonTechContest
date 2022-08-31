namespace OzonTechContest
{
    /// <summary>
    /// Задача "сумма к оплате"
    /// </summary>
    /// <remarks>
    ///Дан массив цен за список продуктов, купленных в магазине. Товары с одинаковой стоимостью считаются одинаковыми.
    /// Требуется посчитать, сколько потребуется заплатить суммарно за весь поход в магазин при условии, что в магазине проводится акция — «купи три одинаковых товара и заплати только за два».
    /// </remarks>
    public sealed class PayableAmount_1
    {
        /// <summary>
        /// Вспомогательный метод для чтения входных данных из консоли
        /// </summary>
        /// <exception cref="ArgumentException"> Ошибка входных данных </exception>
        public void ReadData()
        {
            var totalProductsStr = Console.ReadLine() ?? "0";
            var totalProducts = int.Parse(totalProductsStr);
            var productsStr = Console.ReadLine();
            var productsArrayStr = productsStr?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (totalProducts != productsArrayStr?.Length)
            {
                throw new ArgumentException("Количество цен не соответствует количеству товаров");
            }

            var prices = new List<int>();

            for (var i = 0; i < totalProducts; ++i)
            {
                var price = int.Parse(productsArrayStr[i]);

                prices.Add(price);
            }

            var result = CalcPayableAmount(prices);

            Console.WriteLine(result);
        }

        /// <summary>
        /// Подсчёт итоговой суммы к оплате
        /// </summary>
        /// <param name="prices"> Количество цен товаров в чеке </param>
        /// <returns> Итоговая сумма к оплате </returns>
        /// <remarks>
        /// Time complexity: O(n)
        /// Space complexity: O(n)
        /// </remarks>
        public static double CalcPayableAmount(List<int> prices)
        {
            var productsDict = new Dictionary<int, int>();

            //// Определяем количество одинаковых товаров по одной цене
            foreach (var price in prices)
            {
                if (!productsDict.ContainsKey(price))
                {
                    productsDict.Add(price, 0);
                }

                ++productsDict[price];
            }

            var result = 0;

            foreach (var item in productsDict)
            {
                //// Определяем итоговую сумму по каждому товару, целочисленное деление позволяет определить количество тройных товаров
                result += item.Key * (item.Value - item.Value / 3);
            }

            return result;
        }
    }
}