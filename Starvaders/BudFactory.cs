using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public static class BudFactory
    {
        public static List<CardInstance> GetCardDraft(RunState runInfo, Random random)
        {
            List<CardInstance> cards = new List<CardInstance>();

            cards.AddRange(RNGHelper.GetRandomWeightedCards(runInfo, 1, random, 0.01f, 0.05f, CardTraitFlag.Attack));
            runInfo.AddToBlockList(cards[0]);
            runInfo.SetBlockList();

            cards.AddRange(RNGHelper.GetRandomWeightedCards(runInfo, 1, random, 0.01f, 1f));
            runInfo.AddToBlockList(cards[1]);
            runInfo.SetBlockList();

            cards.AddRange(RNGHelper.GetRandomWeightedCards(runInfo, 1, random, 0.01f, 1f, CardTraitFlag.Move));
            runInfo.AddToBlockList(cards[2]);
            runInfo.SetBlockList();

            cards.AddRange(RNGHelper.GetRandomWeightedCards(runInfo, 2, random, 0.01f, 0.05f));

            cards = cards.Randomize(random).ToList();

            for (int index = 0; index < 5; ++index)
            {
                runInfo.AddToBlockList(cards[index]);
                runInfo.SetBlockList();
            }

            return cards;
        }
    }
}
