namespace AutomataTests
{
    [TestClass]
    public class NFATests1
    {

        private readonly NonDeterministicFiniteAutomaton<char> nfa;

        public NFATests1()
        {

            nfa = AutomatonFactory.GenerateNFA(
                transitions: new FiniteTransition<char>[]
                {
                    new(0, 1, OptSymbol<char>.None),
                    new(1, 2, OptSymbol<char>.None),
                    new(1, 8, OptSymbol<char>.None),
                    new(2, 3, OptSymbol<char>.None),
                    new(2, 5, OptSymbol<char>.None),
                    new(4, 1, OptSymbol<char>.None),
                    new(7, 1, OptSymbol<char>.None),
                    new(12, 1, OptSymbol<char>.None),
                    new(3, 4, 'a'),
                    new(5, 6, 'b'),
                    new(6, 7, 'a'),
                    new(8, 9, 'b'),
                    new(9, 10, 'a'),
                    new(10, 11, 'a'),
                    new(11, 12, 'a')
                },
                acceptingStates: ImmutableHashSet.CreateRange(new List<int> { 0, 3, 7, 12 })
            );

        }

        [TestMethod]
        public void Test0()
        {
            Assert.IsTrue(nfa.Run(Array.Empty<char>()));
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(nfa.Run(new char[] { 'a' }));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.IsTrue(nfa.Run(new char[] { 'a', 'a', 'a', 'a' }));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.IsTrue(nfa.Run(new char[] { 'b', 'a', 'a' }));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.IsTrue(nfa.Run(new char[] { 'b', 'a', 'b', 'a' }));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.IsFalse(nfa.Run(new char[] { 'b', 'a', 'a', 'b' }));
        }

        [TestMethod]
        public void Test6()
        {
            Assert.IsFalse(nfa.Run(new char[] { 'b' }));
        }

    }
}
