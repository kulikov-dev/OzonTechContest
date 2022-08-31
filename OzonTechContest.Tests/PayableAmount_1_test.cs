using System.Collections;

namespace OzonTechContest.Tests
{
    public sealed class PayableAmount_1_test
    {
        [Theory, ClassData(typeof(PayableAmountTestData))]
        public void Check(List<int> inputData, double expected)
        {
            var result = PayableAmount_1.CalcPayableAmount(inputData);

            Assert.Equal(expected, result);
        }
    }

    public sealed class PayableAmountTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new List<int> {1,1,2,2,3,4,5,6,7,8,9,9,9},
                57
            };

            yield return new object[]
            {
                new List<int>(),
                0
            };

            yield return new object[]
            {
                new List<int> {9,9,9},
                18
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
