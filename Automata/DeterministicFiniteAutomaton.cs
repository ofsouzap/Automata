using System;
using System.Collections.Immutable;
using System.Linq;

namespace Automata
{
    public class DeterministicFiniteAutomaton<Symbol> : DeterministicAutomaton<FiniteTransition<Symbol>, Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public DeterministicFiniteAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            FiniteTransition<Symbol>[] transitions)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {
            
        }

        protected override bool CheckTransitionsAreDeterministic()
        {

            HashSet<(int, Symbol)> stateSymPairsUsed = new();

            foreach (FiniteTransition<Symbol> t in transitions)
            {

                (int, Symbol) stateSymPair = (t.startState, t.sym);

                if (stateSymPairsUsed.Contains(stateSymPair))
                    return false;

                stateSymPairsUsed.Add(stateSymPair);

            }

            return true;

        }

        protected FiniteTransition<Symbol>? FindTransition(int startState, Symbol sym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t.sym.Equals(sym));

        public override bool Run(Symbol[] word)
        {

            int currentState = 0;
            Queue<Symbol> symQueue = new(word);

            while (symQueue.Count > 0)
            {

                Symbol sym = symQueue.Dequeue();

                FiniteTransition<Symbol>? t = FindTransition(currentState, sym);

                // If no transitions found, it is assumed that the word should be rejected
                if (t == null)
                    return false;

                currentState = t.endState;

            }

            // Return whether the automaton has ended in an accepting state
            return acceptingStates.Contains(currentState);

        }

    }
}
