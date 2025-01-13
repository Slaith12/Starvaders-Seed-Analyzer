using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer.Content
{
    public class ComponentInfo
    {
        public readonly string name;
        public readonly CharClassFlag classAvailability;
        public readonly ComponentTraitFlag traits;
        public readonly CardTraitFlag addedTraits;
        public readonly Rarity rarity;

        public ComponentInfo(string name, CharClassFlag classAvailability, ComponentTraitFlag traits, CardTraitFlag addedTraits = 0, Rarity rarity = Rarity.Common)
        {
            this.name = name;
            this.classAvailability = classAvailability;
            this.traits = traits;
            this.addedTraits = addedTraits;
            this.rarity = rarity;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
