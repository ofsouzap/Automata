namespace AutomataTests
{
    [TestClass]
    public class DTATests0
    {

        private readonly DeterministicTuringAutomaton<int> dta;

        public DTATests0()
        {

            dta = AutomatonFactory.GenerateDTA(
                transitions: new TuringTransition<int>[]
                {
                    new(0, 1, 0, 1, 2),
                    new(0, 2, 1, 0, 2),
                    new(1, 0, 1, 0, -1),
                    new(2, 0, 0, 1, -1),
                    new(1, 3, 0, 1, 0),
                    new(2, 3, 1, 0, 0)
                },
                acceptingStates: ImmutableHashSet.Create(3),
                blankSymbol: -1
            );

        }

        [TestMethod]
        public void Test0()
        {
            Assert.IsFalse(dta.Run(Array.Empty<int>()));
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(dta.Run(new int[] { 0, 1, 0 }));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.IsFalse(dta.Run(new int[] { 0, 1, 1 }));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.IsTrue(dta.Run(new int[] { 1, 0, 0, 0 }));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.IsFalse(dta.Run(new int[] { 1, 0, 0, 1 }));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.IsTrue(dta.Run(new int[] { 0, 0, 1, 1, 0, 0, 1, 1, 1 }));
        }

    }
}
