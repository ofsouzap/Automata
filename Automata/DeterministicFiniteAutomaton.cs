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

            HashSet<(int, Symbol?)> stateSymPairsUsed = new();

            foreach (FiniteTransition<Symbol> t in transitions)
            {

                (int, Symbol?) stateSymPair = (t.startState, t.sym);

                if (stateSymPairsUsed.Contains(stateSymPair))
                    return false;

                stateSymPairsUsed.Add(stateSymPair);

            }

            return true;

        }

        protected FiniteTransition<Symbol>? FindInstantTransition(int startState)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && !t.sym.Exists, null);

        protected FiniteTransition<Symbol>? FindReadTransition(int startState, Symbol sym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && t.sym.Exists && t.sym.Equals(sym), null);

        public override bool Run(Symbol[] word)
        {

            int currentState = 0;
            Queue<Symbol> symQueue = new(word);

            while (symQueue.Count > 0)
            {

                #region Instant Transitions

                FiniteTransition<Symbol>? insT = FindInstantTransition(currentState);

                if (insT != null)
                {
                    currentState = insT.endState;
                    continue;
                }

                #endregion

                #region Read Transitions

                Symbol sym = symQueue.Dequeue();

                FiniteTransition<Symbol>? t = FindReadTransition(currentState, sym);

                if (t != null)
                {
                    currentState = t.endState;
                    continue;
                }

                #endregion

                // If no transitions were found, the word is rejected
                return false;

            }

            // Return whether the automaton has ended in an accepting state
            return acceptingStates.Contains(currentState);

        }

    }
}
