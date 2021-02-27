using System;
using System.Linq;
using System.Reflection;

namespace Events
{
    public struct Types
    {
        public const string FinalizeInteractionEvent = "finalize-interaction-event";
        public const string PlayerLossEvent = "player-loss-event";
        public const string PlayerDeathEffectFinishedEvent = "player-loss-death-effect-finished-event";
        public const string PlayerDeathEffectResetEvent = "player-loss-death-effect-reset-event";

        /**
         * Contains all the events defined as public constant strings
         */
        internal static readonly string[] Events = GetEventNames();

        /**
         * Returns the all the events defined as public constant strings in the Types struct
         */
        private static string[] GetEventNames()
        {
            return (
                from fi in typeof(Types).GetFields(BindingFlags.Public | BindingFlags.Static)
                where fi.IsLiteral && !fi.IsInitOnly && Type.GetTypeCode(fi.GetValue(null).GetType()) == TypeCode.String
                select fi.GetValue(null).ToString()
            ).ToArray();
        }
    }
}
