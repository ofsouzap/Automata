namespace AutomataTests
{
    [TestClass]
    public class NFATests1
    {

        private readonly NonDeterministicFiniteAutomaton<string> nfa;

        public NFATests1()
        {

            nfa = AutomatonFactory.GenerateNFA(
                transitionsString: "0,1,;1,2,;1,8,;2,3,;2,5,;4,1,;7,1,;12,1,;3,4,a;5,6,b;6,7,a;8,9,b;9,10,a;10,11,a;11,12,a",
                acceptingStates: ImmutableHashSet.CreateRange(new List<int> { 0, 3, 7, 12 })
            );

        }

        [TestMethod]
        public void Test0()
        {
            Assert.IsTrue(nfa.Run(Array.Empty<string>()));
        }

        [TestMethod]
        public void Test1()
        {
            Assert.IsTrue(nfa.Run(new string[] { "a" }));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.IsTrue(nfa.Run(new string[] { "a", "a", "a", "a" }));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.IsTrue(nfa.Run(new string[] { "b", "a", "a" }));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.IsTrue(nfa.Run(new string[] { "b", "a", "b", "a" }));
        }

        [TestMethod]
        public void Test5()
        {
            Assert.IsFalse(nfa.Run(new string[] { "b", "a", "a", "b" }));
        }

        [TestMethod]
        public void Test6()
        {
            Assert.IsFalse(nfa.Run(new string[] { "b" }));
        }

    }
}
