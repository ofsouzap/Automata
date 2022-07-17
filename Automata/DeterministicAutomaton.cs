using System;
using System.Collections.Immutable;

namespace Automata
{
    public abstract class DeterministicAutomaton<Transition, Symbol> : Automaton<Transition, Symbol>
        where Transition : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public DeterministicAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            Transition[] transitions)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {

            if (!CheckTransitionsAreDeterministic())
                throw new ArgumentException("Transitions aren't deterministic", nameof(transitions));

        }

        protected abstract bool CheckTransitionsAreDeterministic();

    }
}
