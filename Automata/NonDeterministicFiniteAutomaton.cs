using System;
using System.Collections.Immutable;

namespace Automata
{
    public class NonDeterministicFiniteAutomaton<Symbol> : NonDeterministicAutomaton<FiniteTransition<Symbol>, Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public NonDeterministicFiniteAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            FiniteTransition<Symbol>[] transitions)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {

        }

        protected FiniteTransition<Symbol>[] FindTransitions(int startState, Symbol sym)
            => GetTransitionsFromState(startState).Where(t => t.sym.Equals(sym)).ToArray();

        public override bool Run(Symbol[] word)
        {

            List<int> currentStates = new() { 0 };

            Queue<Symbol> symQueue = new(word);

            while (symQueue.Count > 0)
            {

                Symbol sym = symQueue.Dequeue();

                List<int> nextStates = new();

                foreach (int state in currentStates)
                {

                    FiniteTransition<Symbol>[] ts = FindTransitions(state, sym);
                    nextStates.AddRange(ts.Select(t => t.endState));

                }

                if (nextStates.Count == 0)
                    return false;

                currentStates = nextStates;
                
            }

            return currentStates.Any(s => acceptingStates.Contains(s));

        }

    }
}
