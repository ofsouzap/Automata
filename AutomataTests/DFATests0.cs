namespace AutomataTests
{
    [TestClass]
    public class DFATests0
    {

        private DeterministicFiniteAutomaton<int> dfa;

        public DFATests0()
        {

            dfa = AutomatonFactory.GenerateDFM(
                transitions: new FiniteTransition<int>[]
                {
                    new(0, 1, 0),
                    new(0, 2, 1),
                    new(1, 3, 0),
                    new(1, 4, 1),
                    new(2, 5, 0),
                    new(2, 5, 1),
                    new(5, 2, 0),
                    new(5, 2, 1),
                    new(3, 3, 0),
                    new(4, 4, 1),
                    new(3, 6, 1),
                    new(4, 6, 0)
                },
                acceptingStates: ImmutableHashSet.CreateRange(new List<int> { 3, 4, 5, 6 })
            );

        }

        [TestMethod]
        public void NoneInputTest()
        {
            Assert.IsFalse(dfa.Run(Array.Empty<int>()));
        }

        [TestMethod]
        public void SingleInputTest0()
        {
            Assert.IsFalse(dfa.Run(new int[] { 0 }));
        }

        [TestMethod]
        public void SingleInputTest1()
        {
            Assert.IsFalse(dfa.Run(new int[] { 1 }));
        }

        [TestMethod]
        public void MoreInputTest0()
        {
            Assert.IsTrue(dfa.Run(new int[] { 0, 0 }));
        }

        [TestMethod]
        public void MoreInputTest1()
        {
            Assert.IsTrue(dfa.Run(new int[] { 0, 1 }));
        }

        [TestMethod]
        public void MoreInputTest2()
        {
            Assert.IsTrue(dfa.Run(new int[] { 1, 0 }));
        }

        [TestMethod]
        public void MoreInputTest3()
        {
            Assert.IsTrue(dfa.Run(new int[] { 1, 1 }));
        }

        [TestMethod]
        public void LoopingInputTest0()
        {
            Assert.IsFalse(dfa.Run(new int[] { 1, 1, 0 }));
        }

        [TestMethod]
        public void LoopingInputTest1()
        {
            Assert.IsFalse(dfa.Run(new int[] { 1, 1, 1 }));
        }

        [TestMethod]
        public void LoopingInputTest2()
        {
            Assert.IsTrue(dfa.Run(new int[] { 1, 1, 1, 0 }));
        }

        [TestMethod]
        public void LoopingInputTest3()
        {
            Assert.IsFalse(dfa.Run(new int[] { 1, 0, 1, 0, 1, 0, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest0()
        {
            Assert.IsFalse(dfa.Run(new int[] { 0, 0, 1, 0 }));
        }

        [TestMethod]
        public void DefaultingInputTest1()
        {
            Assert.IsFalse(dfa.Run(new int[] { 0, 0, 0, 0, 0, 1, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest2()
        {
            Assert.IsFalse(dfa.Run(new int[] { 0, 1, 0, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest3()
        {
            Assert.IsTrue(dfa.Run(new int[] { 0, 1, 1, 1, 0 }));
        }

    }
}