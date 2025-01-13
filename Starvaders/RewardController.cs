using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public static class RewardController
    {
        public static List<CardInstance> GetCardReward(RunState runState, CardTraitFlag trait = CardTraitFlag.AnyTrait)
        {
            Random rng = new Random(runState.modifiedSeed);
            List<CardInstance> cards = RNGHelper.GetRandomWeightedCards(runState, 3, rng, runState.defaultRarityRatio, 0.05f, trait);

            return cards;
        }

        public static List<CardInstance> GetMoveTacticCardReward(RunState runState)
        {
            Random rng = new Random(runState.modifiedSeed);
            List<CardInstance> cards = RNGHelper.GetRandomWeightedCards(runState, 2, rng, runState.defaultRarityRatio, 0.05f, CardTraitFlag.Tactic);
            cards.Add(RNGHelper.GetRandomWeightedCards(runState, 1, rng, runState.defaultRarityRatio, 0.05f, CardTraitFlag.Move).First());
            
            return cards;
        }

        public static List<CardInstance> GetRareCardReward(RunState runState)
        {
            Random rng = new Random(runState.modifiedSeed);
            List <CardInstance> cards = RNGHelper.GetRandomRareCards(runState, 3, rng);

            return cards;
        }

        public static List<ArtifactInfo> GetArtifactReward(RunState runState, Rarity rarity)
        {
            Random rng = new Random(runState.modifiedSeed);
            List<ArtifactInfo> artifacts = RNGHelper.GetRandomArtifacts(runState, 3, rng, rarity);

            return artifacts;
        }

        public static List<(int, ComponentInfo)> GetComponentReward(RunState runState)
        {
            List<CardInstance> cards = runState.cardDeck;
            Random rng = new Random(runState.modifiedSeed);
            List<(int, ComponentInfo)> rewardCandidates = new();
            HashSet<ComponentInfo> existingComponents = new();
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].component != null)
                    continue;
                List<ComponentInfo> possibleComponents = runState.GetAvailableComponents(cards[i])
                    .Where(component => component.rarity == Rarity.Common)
                    .Randomize(rng).ToList();

                if (possibleComponents.Count == 0)
                    continue;

                ComponentInfo chosen = possibleComponents.OrderByDescending(component => existingComponents.Contains(component) ? 0.1f : 1).First();

                if (rewardCandidates.Any(candidate => cards[candidate.Item1].cardInfo == cards[i].cardInfo && candidate.Item2 == chosen))
                    continue;

                rewardCandidates.Add((i, chosen));
                existingComponents.Add(chosen);
            }
            return rewardCandidates
                .WeightedRandomize(candidate => (float)Math.Pow((double)cards[candidate.Item1].rarity + 1.0, 2.0), rng)
                .Take(3).ToList();
        }
    }
}
