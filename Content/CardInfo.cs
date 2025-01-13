using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer.Content
{
    public class CardInfo
    {
        public readonly string name;
        public readonly Rarity rarity;
        public readonly CardTraitFlag traits;
        public readonly CharClassFlag classAvailability;
        public readonly ComponentTraitFlag componentTraitWhitelist;
        public readonly ComponentTraitFlag componentTraitBlacklist;
        public readonly IReadOnlySet<string> componentWhitelistOverride;
        public readonly IReadOnlySet<string> componentBlacklistOverride;

        public bool AllowsComponent(ComponentInfo component)
        {
            if (componentWhitelistOverride.Contains(component.name))
                return true;
            else if (componentBlacklistOverride.Contains(component.name))
                return false;
            else
                return (componentTraitWhitelist & component.traits) != 0
                    && (componentTraitBlacklist & component.traits) == 0;
        }

        public CardInstance CreateInstance(ComponentInfo? component = null)
        {
            return new CardInstance(this, component);
        }

        public CardInfo(string name, Rarity rarity, CardTraitFlag traits, CharClassFlag classAvailability, ComponentTraitFlag componentTraitWhitelist, ComponentTraitFlag componentTraitBlacklist = 0, string[] componentWhitelistOverride = null!, string[] componentBlacklistOverride = null!)
        {
            this.name = name;
            this.rarity = rarity;
            this.traits = traits;
            this.classAvailability = classAvailability;
            this.componentTraitWhitelist = componentTraitWhitelist;
            this.componentTraitBlacklist = componentTraitBlacklist;

            if (componentWhitelistOverride == null)
                componentWhitelistOverride = [];
            this.componentWhitelistOverride = new HashSet<string>(componentWhitelistOverride);
            if (componentBlacklistOverride == null)
                componentBlacklistOverride = [];
            this.componentBlacklistOverride = new HashSet<string>(componentBlacklistOverride);
        }

        public CardInfo(string name, Rarity rarity, CardTraitFlag traits, CharClassFlag classAvailability, string[] componentWhitelist)
            : this(name, rarity, traits, classAvailability, 0, 0, componentWhitelist, []) { }
    }
}
