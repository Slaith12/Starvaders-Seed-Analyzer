using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public class CardInstance(CardInfo card, ComponentInfo? component = null)
    {
        public readonly CardInfo cardInfo = card;
        public ComponentInfo? component = component;

        public string name => cardInfo.name;
        public Rarity rarity => cardInfo.rarity;
        public CardTraitFlag traits => cardInfo.traits | (component != null ? component.addedTraits : 0);

        public bool AllowsComponent(ComponentInfo component) => cardInfo.AllowsComponent(component);

        public static implicit operator CardInfo(CardInstance card) => card.cardInfo;

        public CardInstance Copy()
        {
            CardInstance newCard = new CardInstance(cardInfo);
            newCard.component = this.component;
            return newCard;
        }

        public override string ToString()
        {
            if (component == null)
                return name;
            else
                return $"{name} ({component.name})";
        }
    }
}
