namespace OzonTechContest.Tests
{
    public sealed class LoginValidator_3_test
    {
        [Fact]
        public void Check()
        {
            var solver = new LoginValidator_3();

            Assert.False(solver.IsValid("t"));
            Assert.True(solver.IsValid("Test"));
            Assert.False(solver.IsValid("tESt"));
            Assert.True(solver.IsValid("tt"));
            Assert.True(solver.IsValid("gjklsheh88333--___dsdf"));
            Assert.False(solver.IsValid("sdgnsd*ese"));
            Assert.False(solver.IsValid("tasdfasdfasdfadsfasdfasdfwerwerwerwerwerewfsdfsdfasdfasdfasdf"));
        }
    }
}
