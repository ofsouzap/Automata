namespace AutomataTests
{
    [TestClass]
    public class DPATests0
    {

        private DeterministicPushdownAutomaton<char> dpa;

        public DPATests0()
        {

            dpa = AutomatonFactory.GenerateDPA(
                transitions: new PushdownTransition<char>[]
                {
                    new(0, 0, 'a', 'Z', new char[] { 'Z', 'X' }),
                    new(0, 0, 'a', 'X', new char[] { 'X', 'X' }),
                    new(0, 1, 'b', 'X', Array.Empty<char>()),
                    new(1, 2, 'X', Array.Empty<char>()),
                    new(2, 1, 'b', 'X', Array.Empty<char>()),
                    new(2, 3, 'Z', new char[] { 'Z' })
                },
                stackBottomSymbol: 'Z',
                acceptingStates: ImmutableHashSet.Create(3)
            );

        }


        [TestMethod]
        public void NoneInputTest()
        {
            Assert.IsFalse(dpa.Run(Array.Empty<char>()));
        }

        [TestMethod]
        public void Test0()
        {
            Assert.IsFalse(dpa.Run(new char[] { 'a', 'b' }));
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(dpa.Run(new char[] { 'a', 'a', 'b' }));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.IsFalse(dpa.Run(new char[] { 'a', 'b', 'b' }));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.IsFalse(dpa.Run(new char[] { 'a', 'a', 'b', 'b' }));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.IsFalse(dpa.Run(new char[] { 'b', 'a', 'a', 'a', 'a', 'b', 'b' }));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.IsFalse(dpa.Run(new char[] { 'b', 'b', 'a' }));
        }

        [TestMethod]
        public void Test6()
        {
            Assert.IsTrue(dpa.Run(new char[] { 'a', 'a', 'a', 'a', 'b', 'b' }));
        }

    }
}
