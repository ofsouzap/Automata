using System;
using System.Collections.Immutable;
using System.Linq;

namespace Automata
{
    public abstract class Automaton<Transition, Symbol>
        where Transition : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public readonly ImmutableHashSet<Symbol> alphabet;

        // N.B. state 0 is always used as the initial state

        public readonly int stateCount;

        /// <summary>
        /// Number of state with highest number
        /// </summary>
        public int FinalState => stateCount - 1;

        protected ImmutableHashSet<int> acceptingStates;

        protected readonly Transition[] transitions;

        public Automaton(ImmutableHashSet<Symbol> alphabet, int stateCount, ImmutableHashSet<int> acceptingStates, Transition[] transitions)
        {

            if (stateCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(stateCount));

            this.alphabet = alphabet;
            this.stateCount = stateCount;
            this.acceptingStates = acceptingStates;
            this.transitions = transitions;

            if (!CheckTransitionsUseAlphabet())
                throw new ArgumentException("Transitions don't all use automaton alphabet", nameof(transitions));

            if (!CheckTransitionsInStateRange())
                throw new ArgumentException("Transitions don't all use valid states for start and end state", nameof(transitions));

        }

        /// <summary>
        /// Runs the automaton from the initial state to determine if it accepts the provided word
        /// </summary>
        public abstract bool Run(Symbol[] word);

        #region Transition Checking

        protected bool CheckTransitionsUseAlphabet()
            => transitions.All(t => CheckTransitionUsesAlphabet(t));

        protected bool CheckTransitionUsesAlphabet(Transition t)
            => alphabet.Contains(t.sym);

        protected bool CheckTransitionsInStateRange()
            => transitions.All(t => CheckTransitionInStateRange(t));

        protected bool CheckTransitionInStateRange(Transition t)
            => t.startState >= 0 && t.startState <= FinalState && t.endState >= 0 && t.endState <= FinalState;

        #endregion

        protected Transition[] GetTransitionsFromState(int state)
            => transitions
            .Where(t => t.startState == state)
            .ToArray();

        protected Transition[] GetTransitionsToState(int state)
            => transitions
            .Where(t => t.endState == state)
            .ToArray();

    }
}
