using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer.Content
{
    public class ArtifactInfo
    {
        public readonly string name;
        public readonly Rarity rarity;
        public readonly CharClassFlag classAvailability;

        public ArtifactInfo(string name, Rarity rarity, CharClassFlag classAvailability)
        {
            this.name = name;
            this.rarity = rarity;
            this.classAvailability = classAvailability;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
