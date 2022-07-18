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

        public static DeterministicFiniteAutomaton<string> GenerateDFA(string transitionsString,
            ImmutableHashSet<int> acceptingStates)
        {

            string[] tStrings = transitionsString.Split(';');

            FiniteTransition<string>[] transitions = new FiniteTransition<string>[tStrings.Length];

            for (int i = 0; i < tStrings.Length; i++)
            {

                string tString = tStrings[i];

                if (Parsing.TryParseFiniteTransition(tString, out FiniteTransition<string>? t) && t != null)
                    transitions[i] = t;
                else
                    throw new ArgumentException("Invalid transition provided");


            }

            return GenerateDFA(transitions, acceptingStates);

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

        public static DeterministicPushdownAutomaton<string> GenerateDPA(string transitionsString,
            string stackBottomSymbol,
            ImmutableHashSet<int> acceptingStates)
        {

            string[] tStrings = transitionsString.Split(';');

            PushdownTransition<string>[] transitions = new PushdownTransition<string>[tStrings.Length];

            for (int i = 0; i < tStrings.Length; i++)
            {

                string tString = tStrings[i];

                if (Parsing.TryParsePushdownTransition(tString, out PushdownTransition<string>? t) && t != null)
                    transitions[i] = t;
                else
                    throw new ArgumentException("Invalid transition provided");


            }

            return GenerateDPA(transitions, stackBottomSymbol, acceptingStates);

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

        public static NonDeterministicFiniteAutomaton<string> GenerateNFA(string transitionsString,
            ImmutableHashSet<int> acceptingStates)
        {

            string[] tStrings = transitionsString.Split(';');

            FiniteTransition<string>[] transitions = new FiniteTransition<string>[tStrings.Length];

            for (int i = 0; i < tStrings.Length; i++)
            {

                string tString = tStrings[i];

                if (Parsing.TryParseFiniteTransition(tString, out FiniteTransition<string>? t) && t != null)
                    transitions[i] = t;
                else
                    throw new ArgumentException("Invalid transition provided");


            }

            return GenerateNFA(transitions, acceptingStates);

        }

        public static DeterministicTuringAutomaton<Symbol> GenerateDTA<Symbol>(TuringTransition<Symbol>[] transitions,
            ImmutableHashSet<int> acceptingStates,
            Symbol blankSymbol)
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
                blankSymbol: blankSymbol
            );

        }

        public static DeterministicTuringAutomaton<string> GenerateDTA(string transitionsString,
            ImmutableHashSet<int> acceptingStates,
            string blankSymbol)
        {

            string[] tStrings = transitionsString.Split(';');

            TuringTransition<string>[] transitions = new TuringTransition<string>[tStrings.Length];

            for (int i = 0; i < tStrings.Length; i++)
            {

                string tString = tStrings[i];

                if (Parsing.TryParseTuringTransition(tString, out TuringTransition<string>? t) && t != null)
                    transitions[i] = t;
                else
                    throw new ArgumentException("Invalid transition provided");


            }

            return GenerateDTA(transitions, acceptingStates, blankSymbol);

        }

    }
}
