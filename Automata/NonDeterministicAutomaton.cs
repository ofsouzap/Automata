using System;
using System.Collections.Immutable;

namespace Automata
{
    public abstract class NonDeterministicAutomaton<Transition, Symbol> : Automaton<Transition, Symbol>
        where Transition : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public NonDeterministicAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            Transition[] transitions)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {

        }

    }
}
