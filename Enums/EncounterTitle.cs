using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvaders_Seed_Analyzer.Enums
{
    public struct EncounterTitle(EncounterPrefix prefix, EncounterSuffix suffix)
    {
        public EncounterPrefix prefix = prefix;
        public EncounterSuffix suffix = suffix;

        public static EncounterTitle GetRandomTitle(Random random)
        {
            return new EncounterTitle(Enum.GetValues(typeof(EncounterPrefix)).Cast<EncounterPrefix>().ToList().PickRandom(random), Enum.GetValues(typeof(EncounterSuffix)).Cast<EncounterSuffix>().ToList().PickRandom(random));
        }

        private static readonly int[] swarmSuffixIndices = [0, 1, 2, 3, 4];
        public static EncounterSuffix GetRandomSwarmSuffix(Random random)
        {
            return (EncounterSuffix)(swarmSuffixIndices.PickRandom(random));
        }

        public override string ToString()
        {
            string prefixString = prefix switch
            {
                EncounterPrefix.Prefix1  => "Atelane",
                EncounterPrefix.Prefix2  => "Shin-Tokyo",
                EncounterPrefix.Prefix3  => "Sahara",
                EncounterPrefix.Prefix4  => "Polaria",
                EncounterPrefix.Prefix5  => "Axolandis",
                EncounterPrefix.Prefix6  => "Nova York",
                EncounterPrefix.Prefix7  => "Unueco",
                EncounterPrefix.Prefix8  => "Esperanto",
                EncounterPrefix.Prefix9  => "Yangtze",
                EncounterPrefix.Prefix10 => "Starlight Bay",
                EncounterPrefix.Prefix11 => "Kwanzari",
                EncounterPrefix.Prefix12 => "Elysia",
                EncounterPrefix.Prefix13 => "Liberty City",
                EncounterPrefix.Prefix14 => "Solaris",
                EncounterPrefix.Prefix15 => "Sky Valley",
                EncounterPrefix.Prefix16 => "Arcadia",
                EncounterPrefix.Prefix17 => "Azuhai",
                EncounterPrefix.Prefix18 => "Ellatis",
                EncounterPrefix.Prefix19 => "Port Sohae",
                EncounterPrefix.Prefix20 => "Salrich",
                _ => "InvalidPrefix"
            };

            string suffixString = suffix switch
            {
                EncounterSuffix.Suffix1 =>  "Under Attack!",
                EncounterSuffix.Suffix2 =>  "In Danger!",
                EncounterSuffix.Suffix3 =>  "Invasion!",
                EncounterSuffix.Suffix4 =>  "Needs Help!",
                EncounterSuffix.Suffix5 =>  "Event!",
                EncounterSuffix.Suffix6 =>  "Code Red!",
                EncounterSuffix.Suffix7 =>  "Final Stand!",
                EncounterSuffix.Suffix8 =>  "Must Win!",
                EncounterSuffix.Suffix9 =>  "Contact!",
                EncounterSuffix.Suffix10 => "Battle!",
                _ => "InvalidSuffix"
            };
            return $"{prefixString} {suffixString}";
        }

        public string ToString(EncounterType type, string enemyName)
        {
            return type switch
            {
                EncounterType.Swarm => enemyName + suffix switch
                {
                    EncounterSuffix.Suffix1 => " Swarm",
                    EncounterSuffix.Suffix2 => " Horde",
                    EncounterSuffix.Suffix3 => " Legion",
                    EncounterSuffix.Suffix4 => " Gang",
                    EncounterSuffix.Suffix5 => " Party",
                    _ => " InvalidSuffix"
                },
                EncounterType.Cloudy => "Cloudy",
                EncounterType.Holes => "Holes!",
                EncounterType.Void => "Void Zone",
                _ => ToString()
            };
        }
    }

    public enum EncounterPrefix
    {
        Prefix1,
        Prefix2,
        Prefix3,
        Prefix4,
        Prefix5,
        Prefix6,
        Prefix7,
        Prefix8,
        Prefix9,
        Prefix10,
        Prefix11,
        Prefix12,
        Prefix13,
        Prefix14,
        Prefix15,
        Prefix16,
        Prefix17,
        Prefix18,
        Prefix19,
        Prefix20,
    }

    public enum EncounterSuffix
    {
        Suffix1,
        Suffix2,
        Suffix3,
        Suffix4,
        Suffix5,
        Suffix6,
        Suffix7,
        Suffix8,
        Suffix9,
        Suffix10,
    }
}
