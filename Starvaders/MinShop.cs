using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public class MinShop
    {
        public IReadOnlyList<MinCard> cards;
        public IReadOnlyList<MinArtifact> artifacts;
        public IReadOnlyList<object> gachaSequence;

        public MinShop(RunState runState, bool truncateGacha = true)
        {
            List<MinCard> cards = new();
            List<MinArtifact> artifacts = new();
            List<object> gachaSequence = new();

            Random rng = new Random(runState.modifiedSeed);
            if (runState.artifactDeck.Contains(runState.campaign.artifacts["Wormhole"]))
                rng = new Random(rng.Next());
            if (runState.artifactDeck.Contains(runState.campaign.artifacts["Lizard Soul"]))
                rng = new Random(rng.Next());

            List<CardInstance> randomCards = RNGHelper.GetRandomWeightedCards(runState, 4, rng, runState.defaultRarityRatio, 0.1f);
            List<ArtifactInfo> randomArtifacts = RNGHelper.GetRandomArtifacts(runState, 2, rng, Rarity.Common);

            foreach(CardInstance card in randomCards)
            {
                runState.AddToBlockList(card);
                MinCard minCard = new MinCard();
                minCard.cardInfo = card.cardInfo;
                minCard.component = card.component;
                minCard.price = card.rarity switch { Rarity.Common => 40, Rarity.Rare => 60, Rarity.Legendary => 80, _ => 0 };
                minCard.price += rng.Next(0, 10) + runState.actNumber * (10 + runState.GetRunModValue(RunMod.ShopPriceScaling));
                minCard.num = 1;

                float num = (float)rng.NextDouble();
                if(num < 0.05)
                {
                    minCard.num = 2;
                    minCard.price += 25;
                }
                else if(num < 0.15)
                {
                    minCard.price -= 20;
                    minCard.sale = true;
                }
                cards.Add(minCard);
            }

            foreach(ArtifactInfo artifact in randomArtifacts)
            {
                runState.AddToBlockList(artifact);
                MinArtifact minArtifact = new MinArtifact();
                minArtifact.artifact = artifact;
                minArtifact.price = rng.Next(90, 110) + runState.actNumber * (10 + runState.GetRunModValue(RunMod.ShopPriceScaling) * 2);
                artifacts.Add(minArtifact);
            }

            gachaSequence = RNGHelper.GetRandomWeightedCards(runState, 50, rng, runState.defaultRarityRatio)
                .Select(a=>(object)a)
                .Union(runState.GetAvailableArtifacts()
                    .Where(artifact => artifact.rarity == Rarity.Common)
                    .Where(artifact => !runState.artifactDeck.Contains(artifact))
                    .PickRandom(5, rng))
                .Randomize(rng)
                .Where(artifact => !truncateGacha || !(artifact is ArtifactInfo && artifacts.Select(minItem => minItem.artifact).Contains(artifact))).ToList();

            this.cards = cards;
            this.artifacts = artifacts;
            this.gachaSequence = gachaSequence;
        }

        public override string ToString()
        {
            string ret = "Cards:";
            foreach (MinCard card in cards)
                ret += "\n" + card.ToString();

            ret += "\n\nArtifacts:";
            foreach (MinArtifact artifact in artifacts)
                ret += "\n" + artifact.ToString();

            ret += "\n\nGacha Sequence (First 5):";
            for(int i = 0; i < 5; i++)
            {
                ret += "\n" + gachaSequence[i].ToString();
            }
            return ret;
        }
    }

    public struct MinCard(CardInfo card, int price, ComponentInfo? component = null, int num = 1)
    {
        public CardInfo cardInfo = card;
        public ComponentInfo? component = component;
        public int price = price;
        public bool sale = false;
        public int num = num;

        public override string ToString()
        {
            string ret = "";
            if (num != 1)
                ret += $"{num}x ";
            ret += cardInfo.name;
            if (component != null)
                ret += $" ({component.name})";
            ret += $" [Cost: {price}00 stars";
            if (sale)
                ret += " (Sale!)";
            ret += "]";
            return ret;
        }
    }

    public struct MinArtifact(ArtifactInfo artifact, int price)
    {
        public ArtifactInfo artifact = artifact;
        public int price = price;

        public override string ToString()
        {
            return $"{artifact.name} [Cost: {price}00 stars]";
        }
    }
}
