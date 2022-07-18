using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Automata
{
    public static class Parsing
    {

        /* 
         * All transitions parsed using this will be created using strings as the symbol type.
         * For any transition, the first three parameters/values are the start state index, the end state index and the symbol required respectively.
         * Symbols must only be comprised of digits and lower- or upper-case characters.
         * Stack symbol replacements for pushdown automata have each symbol separated by a colon.
         * An empty string is used to represent an instant transition
         */

        private static readonly Regex finiteTransitionPattern = new(@"^(?<start>\d+),(?<end>\d+),(?<sym>[0-9A-z]*)$");
        private static readonly Regex pushdownTransitionPattern = new(@"^(?<start>\d+),(?<end>\d+),(?<sym>[0-9A-z]*),(?<stackIn>[0-9A-z]+),(?<stackOut>([0-9A-z]+)(:[0-9A-z]+)*)$");
        private static readonly Regex turingTransitionPattern = new(@"^(?<start>\d+),(?<end>\d+),(?<sym>[0-9A-z]*),(?<writeSym>[0-9A-z]+),(?<headMove>-?\d+)$");

        public static bool TryParseFiniteTransition(string line,
            out FiniteTransition<string>? t)
        {

            Match m = finiteTransitionPattern.Match(line);

            if (!m.Success)
            {
                t = default;
                return false;
            }

            int startState, endState;
            string sym;

            if (!int.TryParse(m.Groups["start"].Value, out startState) || !int.TryParse(m.Groups["end"].Value, out endState))
            {
                t = default;
                return false;
            }

            sym = m.Groups["sym"].Value;

            t = new(startState, endState, string.IsNullOrEmpty(sym) ? OptSymbol<string>.None : new(sym));

            return true;

        }

        public static bool TryParsePushdownTransition(string line,
            out PushdownTransition<string>? t)
        {

            Match m = pushdownTransitionPattern.Match(line);

            if (!m.Success)
            {
                t = default;
                return false;
            }

            int startState, endState;
            string sym, stackIn;
            string[] stackOut;

            if (!int.TryParse(m.Groups["start"].Value, out startState) || !int.TryParse(m.Groups["end"].Value, out endState))
            {
                t = default;
                return false;
            }

            sym = m.Groups["sym"].Value;
            stackIn = m.Groups["stackIn"].Value;

            string stackOutStr = m.Groups["stackOut"].Value;
            stackOut = stackOutStr.Split(':');

            t = new(startState, endState, string.IsNullOrEmpty(sym) ? OptSymbol<string>.None : new(sym), stackIn, stackOut);

            return true;

        }

        public static bool TryParseTuringTransition(string line,
            out TuringTransition<string>? t)
        {

            Match m = turingTransitionPattern.Match(line);

            if (!m.Success)
            {
                t = default;
                return false;
            }

            int startState, endState, headMove;
            string sym, writeSym;

            if (!int.TryParse(m.Groups["start"].Value, out startState)
                || !int.TryParse(m.Groups["end"].Value, out endState)
                || !int.TryParse(m.Groups["headMove"].Value, out headMove))
            {
                t = default;
                return false;
            }

            sym = m.Groups["sym"].Value;
            writeSym = m.Groups["writeSym"].Value;

            t = new(startState, endState, string.IsNullOrEmpty(sym) ? OptSymbol<string>.None : new(sym), writeSym, headMove);

            return true;

        }

    }
}
