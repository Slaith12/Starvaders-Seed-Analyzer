using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;
using System;

namespace Starvaders_Seed_Analyzer
{
    public static class RNGHelper
    {
        public static int GetModifiedSeed(int runSeed, int actNumber, int dayNumber, int chronoTokenAmount)
        {
            string s = "" + runSeed + actNumber + dayNumber + chronoTokenAmount;
            int result = 0;
            while (!int.TryParse(s, out result))
                s = s.Remove(s.Length - 1, 1);
            return result;
        }

        public static List<CardInstance> GetRandomWeightedCards(RunState run, int amount, Random random, float rarityRatio, float componentChance = 0.0f, CardTraitFlag hasTrait = CardTraitFlag.AnyTrait)
        {
            List<CardInstance> list = run.GetAvailableCards()
                .Where(cardModel => GetAcceptedCardRarities(cardModel.rarity))
                .WeightedRandomize(cardModel => GetCardRarityWeight(cardModel.rarity, rarityRatio), random)
                .Where(cardModel => !run.cardBlockList.Contains(cardModel))
                .Where(cardModel => (cardModel.traits & hasTrait) != 0)
                .Take(amount)
                .Select(cardInfo => cardInfo.CreateInstance()).ToList();

            GiveComponentRandomly(run, random, componentChance, list);
            if (list.Count != amount && run.cardBlockList.Count != 0)
            {
                run.cardBlockList.Clear();
                return GetRandomWeightedCards(run, amount, random, rarityRatio, componentChance, hasTrait);
            }
            return list;
        }

        public static List<CardInstance> GetRandomRareCards(RunState run, int amount, Random random, float componentChance = 0)
        {
            List<CardInstance> list = run.GetAvailableCards()
                .Where(card => card.rarity == Rarity.Rare || card.rarity == Rarity.Legendary)
                .Where(card => !run.cardBlockList.Contains(card))
                .PickRandom(amount, random)
                .Select(card => card.CreateInstance()).ToList();
            GiveComponentRandomly(run, random, componentChance, list);
            return list;
        }

        private static float GetCardRarityWeight(Rarity rarity, float rarityRatio)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return 1.5f - rarityRatio;
                case Rarity.Rare:
                    return (float)(0.5 * ((double)rarityRatio * (double)rarityRatio) + 0.5);
                case Rarity.Legendary:
                    return (float)(1.0000000116861E-07 + (double)rarityRatio * (double)rarityRatio);
                default:
                    return 1E-07f;
            }
        }

        private static void GiveComponentRandomly(RunState run, Random random, float componentChance, List<CardInstance> cards)
        {
            cards.ForEach(cardModel =>
            {
                if (random.NextDouble() >= (double)componentChance)
                    return;
                GiveRandomComponent(run, cardModel, Rarity.Common, random);
            });
        }

        public static bool GiveRandomComponent(RunState run, CardInstance card, Rarity rarity, Random random)
        {
            List<ComponentInfo> list = run.GetAvailableComponents(card).Where(component => component.rarity == rarity).Randomize(random).ToList();
            if (list.Count <= 0)
                return false;
            card.component = list.FirstOrDefault();
            return true;
        }

        public static bool GetAcceptedCardRarities(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return true;
                case Rarity.Rare:
                    return true;
                case Rarity.Legendary:
                    return true;
                default:
                    return false;
            }
        }

        public static List<ArtifactInfo> GetRandomArtifacts(RunState runState, int amount, Random random, Rarity rarity)
        {
            return runState.GetAvailableArtifacts()
                .Where(artifact => artifact.rarity == rarity)
                .WeightedRandomize(artifact => (artifact.classAvailability != CharClassFlag.Neutral && artifact.classAvailability != CharClassFlag.Pack) ? 1.5f : 1, random)
                .Where(artifact => !runState.artifactDeck.Contains(artifact))
                .Where(artifact => !runState.artifactBlockList.Contains(artifact))
                .Take(amount).ToList();
        }

        #region Enumerable Extensions

        public static T PickRandom<T>(this IEnumerable<T> source, Random random) => source.PickRandom(1, random).Single();

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count, Random random, bool withReplacement = false)
        {
            if (!withReplacement)
                return source.Randomize(random).Take(count);
            List<T> objList = new List<T>();
            for (int index = 0; index < count; ++index)
                objList.Add(source.Randomize(random).ToList().First());
            return objList;
        }

        public static T PickRandomWeighted<T>(this IEnumerable<T> source, Func<T, float> weightSelector, Random random)
        {
            return source.PickRandomWeighted(weightSelector, 1, random).Single();
        }

        public static IEnumerable<T> PickRandomWeighted<T>(this IEnumerable<T> source, Func<T, float> weightSelector, int count, Random random, bool withReplacement = false)
        {
            if (!withReplacement)
                return source.WeightedRandomize(weightSelector, random).Take(count);
            List<T> objList = new List<T>();
            for (int index = 0; index < count; ++index)
                objList.Add(source.WeightedRandomize(weightSelector, random).ToList().First());
            return objList;
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, Random random)
        {
            return source.OrderBy(item => random.NextDouble());
        }

        public static IEnumerable<T> WeightedRandomize<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector, Random random)
        {
            List<T> list = sequence.ToList();
            List<T> objList = new List<T>();
            while (list.Any())
            {
                float num1 = list.Sum(weightSelector);
                float num2 = (float)random.NextDouble() * num1;
                float num3 = 0.0f;
                foreach (var data in list.Select(weightedItem => new { Value = weightedItem, Weight = weightSelector(weightedItem) }))
                {
                    if ((double)data.Weight == 0.0)
                        throw new Exception("Weighted random shouldn't have weights of 0");
                    num3 += data.Weight;
                    if ((double)num3 > (double)num2)
                    {
                        T obj = data.Value;
                        objList.Add(obj);
                        list.Remove(obj);
                        break;
                    }
                }
            }
            return objList;
        }

        #endregion
    }
}