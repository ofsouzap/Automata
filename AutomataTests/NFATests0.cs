namespace AutomataTests
{
    [TestClass]
    public class NFATests0
    {

        private NonDeterministicFiniteAutomaton<int> nfa;

        public NFATests0()
        {

            nfa = AutomatonFactory.GenerateNFA(
                transitions: new FiniteTransition<int>[]
                {
                    new(0, 1, 0),
                    new(0, 4, 1),
                    new(0, 2, 0),
                    new(1, 1, 1),
                    new(1, 4, 0),
                    new(2, 4, 1),
                    new(2, 2, 0),
                    new(1, 3, 0),
                    new(4, 3, 0),
                    new(4, 3, 1),
                    new(3, 4, 0),
                    new(3, 4, 1),
                    new(2, 3, 1)
                },
                acceptingStates: ImmutableHashSet.Create(3)
            );

        }

        [TestMethod]
        public void NoneInputTest()
        {
            Assert.IsFalse(nfa.Run(Array.Empty<int>()));
        }

        [TestMethod]
        public void SingleInputTest0()
        {
            Assert.IsFalse(nfa.Run(new int[] { 0 }));
        }

        [TestMethod]
        public void SingleInputTest1()
        {
            Assert.IsFalse(nfa.Run(new int[] { 1 }));
        }

        [TestMethod]
        public void MoreInputTest0()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 0 }));
        }

        [TestMethod]
        public void MoreInputTest1()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 1 }));
        }

        [TestMethod]
        public void MoreInputTest2()
        {
            Assert.IsTrue(nfa.Run(new int[] { 1, 0 }));
        }

        [TestMethod]
        public void MoreInputTest3()
        {
            Assert.IsTrue(nfa.Run(new int[] { 1, 1 }));
        }

        [TestMethod]
        public void LoopingInputTest0()
        {
            Assert.IsFalse(nfa.Run(new int[] { 1, 1, 0 }));
        }

        [TestMethod]
        public void LoopingInputTest1()
        {
            Assert.IsFalse(nfa.Run(new int[] { 1, 1, 1 }));
        }

        [TestMethod]
        public void LoopingInputTest2()
        {
            Assert.IsTrue(nfa.Run(new int[] { 1, 1, 1, 0 }));
        }

        [TestMethod]
        public void LoopingInputTest3()
        {
            Assert.IsFalse(nfa.Run(new int[] { 1, 0, 1, 0, 1, 0, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest0()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 0, 1, 0 }));
        }

        [TestMethod]
        public void DefaultingInputTest1()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 0, 0, 0, 0, 1, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest2()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 1, 0, 1 }));
        }

        [TestMethod]
        public void DefaultingInputTest3()
        {
            Assert.IsTrue(nfa.Run(new int[] { 0, 1, 1, 1, 0 }));
        }

    }
}
