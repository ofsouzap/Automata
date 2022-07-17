using System.Collections.Immutable;

namespace Automata
{
    public static class AutomatonFactory
    {

        private static void FindAutomatonDetailsFromTransitions<Symbol>(Transition<Symbol>[] transitions,
            out ImmutableHashSet<Symbol> alphabet,
            out int maximumState)
            where Symbol : IEquatable<Symbol>
        {

            HashSet<Symbol> alphabetM = new();
            maximumState = 0;

            foreach (Transition<Symbol> t in transitions)
            {

                if (t.sym.Exists && !alphabetM.Contains(t.sym))
                    alphabetM.Add(t.sym);

                if (t.startState > maximumState)
                    maximumState = t.startState;

                if (t.endState > maximumState)
                    maximumState = t.endState;

            }

            alphabet = ImmutableHashSet.CreateRange(alphabetM);

        }

        public static DeterministicFiniteAutomaton<Symbol> GenerateDFA<Symbol>(FiniteTransition<Symbol>[] transitions,
            ImmutableHashSet<int> acceptingStates)
            where Symbol : IEquatable<Symbol>
        {

            FindAutomatonDetailsFromTransitions(transitions,
                out ImmutableHashSet<Symbol> alphabet,
                out int maximumState);

            return new(
                alphabet: alphabet,
                stateCount: maximumState + 1,
                acceptingStates: acceptingStates,
                transitions: transitions
            );

        }

        public static DeterministicPushdownAutomaton<Symbol> GenerateDPA<Symbol>(PushdownTransition<Symbol>[] transitions,
            Symbol stackBottomSymbol,
            ImmutableHashSet<int> acceptingStates)
            where Symbol : IEquatable<Symbol>
        {

            FindAutomatonDetailsFromTransitions(transitions,
                out ImmutableHashSet<Symbol> alphabet,
                out int maximumState);

            return new(
                alphabet: alphabet,
                stateCount: maximumState + 1,
                acceptingStates: acceptingStates,
                transitions: transitions,
                stackBottomSymbol: stackBottomSymbol
            );

        }

        public static NonDeterministicFiniteAutomaton<Symbol> GenerateNFA<Symbol>(FiniteTransition<Symbol>[] transitions,
            ImmutableHashSet<int> acceptingStates)
            where Symbol : IEquatable<Symbol>
        {

            FindAutomatonDetailsFromTransitions(transitions,
                out ImmutableHashSet<Symbol> alphabet,
                out int maximumState);

            return new(
                alphabet: alphabet,
                stateCount: maximumState + 1,
                acceptingStates: acceptingStates,
                transitions: transitions
            );

        }

    }
}
