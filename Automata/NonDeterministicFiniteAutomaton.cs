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

        protected FiniteTransition<Symbol>[] FindInstantTransitions(int startState)
            => GetTransitionsFromState(startState).Where(t => !t.sym.Exists).ToArray();

        protected FiniteTransition<Symbol>[] FindReadTransitions(int startState, Symbol sym)
            => GetTransitionsFromState(startState).Where(t => t.sym.Exists && t.sym.Equals(sym)).ToArray();

        public override bool Run(Symbol[] word)
        {

            HashSet<int> currentStates = new() { 0 };

            Queue<Symbol> symQueue = new(word);

            while (true)
            {

                #region Instant Transitions

                HashSet<int> nextInstantStates = new();

                foreach (int state in currentStates)
                {

                    nextInstantStates.Add(state);

                    FiniteTransition<Symbol>[] ts = FindInstantTransitions(state);

                    foreach (int s in ts.Select(t => t.endState))
                        nextInstantStates.Add(s);

                }

                // Don't proceed to read transitions in this loop if possible states was changed so that more instant transitions can be done if needed before looking at read transitions
                if (!currentStates.SetEquals(nextInstantStates))
                {
                    currentStates = nextInstantStates;
                    continue;
                }

                #endregion

                if (symQueue.Count == 0)
                    break;

                #region Read Transitions

                HashSet<int> nextReadStates = new();

                Symbol sym = symQueue.Dequeue();

                foreach (int state in currentStates)
                {

                    FiniteTransition<Symbol>[] ts = FindReadTransitions(state, sym);

                    foreach (int s in ts.Select(t => t.endState))
                        nextReadStates.Add(s);

                }

                if (nextReadStates.Count == 0)
                    return false;

                currentStates = nextReadStates;

                #endregion

            }

            return currentStates.Any(s => acceptingStates.Contains(s));

        }

    }
}
