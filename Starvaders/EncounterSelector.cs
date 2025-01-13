using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public static class EncounterSelector
    {
        public static List<EncounterInfo> CreateEncounters(RunState runState)
        {
            Random rng = new Random(runState.modifiedSeed);
            int roomAmount = new List<int>() { 2, 2, 2, 3, 3 }.PickRandom(rng);
            int actNumber = runState.actNumber;
            int dayNumber = runState.dayNumber;
            int panelHolderSetting = rng.Next(0, 3); //used for in-game visuals only

            List<EncounterTemplate> rooms = runState.campaign.GetRoomTemplates(actNumber, dayNumber, roomAmount, rng);
            new List<int>() { 1, 2 }.PickRandom(2, rng).ToList(); //another rng call for in-game visuals only
            List<EncounterInfo> options = new List<EncounterInfo>();
            for (int index = 0; index < rooms.Count; ++index)
            {
                EncounterInfo currentOption = InitTemplate(rooms[index], runState, rng, index == 2, GetExistingEnemies(runState.campaign, options), GetExistingRewards(options));
                options.Add(currentOption);
            }
            //... (Special Rewards)
            if (runState.actNumber == 1 && runState.dayNumber == 1)
                return options;
            if (runState.dayNumber != 1 || runState.GetRunModValue(RunMod.ActDoomHealBool) == 0)
            {
                List<EncounterInfo> doomReductionCandidates = options
                    .Where(option => option.rewardType != RewardType.RareCard && option.rewardType != RewardType.Artifact && option.rewardType != RewardType.Special)
                    .Randomize(rng).ToList();
                if(doomReductionCandidates.Count > 0)
                    doomReductionCandidates.First().deductsDoom = true;
            }
            return options;
        }

        private static List<EnemyInfo> GetExistingEnemies(CampaignSettings campaign, List<EncounterInfo> currentEncounters)
        {
            List<EnemyInfo> enemyTypes = new List<EnemyInfo>();
            foreach (EncounterInfo encounter in currentEncounters)
            {
                enemyTypes.AddRange(encounter.enemyList.Distinct());
            }
            List<EnemyInfo> existingEnemies = new List<EnemyInfo>();
            foreach (EnemyInfo enemyType in enemyTypes)
            {
                if (enemyType.name.EndsWith("Plus"))
                {
                    if (campaign.enemies.TryGetValue(enemyType.name.Replace("Plus", ""), out var result))
                        existingEnemies.Add(result);
                }
                else
                    existingEnemies.Add(enemyType);
            }
            return existingEnemies;
        }

        private static List<RewardType> GetExistingRewards(List<EncounterInfo> currentEncounters)
        {
            List<RewardType> existingRewards = new List<RewardType>();
            foreach (EncounterInfo encounter in currentEncounters)
            {
                existingRewards.Add(encounter.rewardType);
            }
            return existingRewards;
        }

        private static EncounterInfo InitTemplate(EncounterTemplate template, RunState runState, Random random, bool isBottomChoice, List<EnemyInfo> usedEnemies, List<RewardType> usedRewards)
        {
            List<EnemyInfo> enemyList = new();
            List<(EnemyInfo, int)> list1 = FilterEnemyCounts(template, runState.campaign, random)
                //.Where((enemyCountData => !UnlockManager.IsLocked(enemyCountData.EnemyName))) [technically this line can matter, except FilterEnemyCounts just returns enemyCountData right now so not really]
                .PickRandomWeighted(enemyCountData => usedEnemies.Contains(enemyCountData.Item1) ? 0.5f : 1f, template.enemyAmount, random).ToList();
            List<int> list2 = new List<int>() { -1, 0, 2 }.Randomize(random).ToList();
            for (int index1 = 0; index1 < list1.Count; ++index1)
            {
                (EnemyInfo, int) enemyCountData = list1[index1];
                //... (Advanced Invaders)
                for (int index2 = 0; index2 < Math.Max(2, enemyCountData.Item2 + list2[index1 % 3]); ++index2)
                    enemyList.Add(enemyCountData.Item1);
            }
            enemyList = enemyList.Randomize(random).ToList();
            EncounterTitle randomTitle = EncounterTitle.GetRandomTitle(random);
            RewardType reward = PickReward(template, runState, random, isBottomChoice, usedRewards);

            if (template.type == EncounterType.Swarm)
            {
                randomTitle.suffix = EncounterTitle.GetRandomSwarmSuffix(random);
            }
            return new EncounterInfo(template, randomTitle, enemyList, reward);
        }

        private static List<(EnemyInfo, int)> FilterEnemyCounts(EncounterTemplate template, CampaignSettings campaign, Random random)
        {
            List<(EnemyInfo, int)> list = template.enemyCountData.ToList(); //creates a copy
            foreach ((EnemyInfo enemyName1, EnemyInfo enemyName2) in campaign.GetImpossibleEnemyCombinations())
            {
                EnemyInfo firstEnemy;
                EnemyInfo secondEnemy;
                if (random.NextDouble() > 0.5)
                {
                    firstEnemy = enemyName1;
                    secondEnemy = enemyName2;
                }
                else
                {
                    firstEnemy = enemyName2;
                    secondEnemy = enemyName1;
                }
                if (list.Any(newEnemyCountData => newEnemyCountData.Item1 == firstEnemy))
                    list.RemoveAll(item => item.Item1 == secondEnemy);
            }
            return list;
        }

        private static RewardType PickReward(EncounterTemplate template, RunState runState, Random random, bool isBottomRoom, List<RewardType> usedRewards)
        {
            List<(RewardType, float)> source = template.rewardWeights.Where(reward => RewardFilter(runState, reward.Item1, isBottomRoom)).ToList();
            List<(RewardType, float)> list = source.Where(reward => !usedRewards.Contains(reward.Item1)).ToList();
            if (list.Count > 0)
                source = list;
            RewardType rewardName = source.PickRandomWeighted(reward => reward.Item2, random).Item1;
            if (rewardName == RewardType.Special)
                runState.remainingSpecialRewards--;
            return rewardName;
            //... (Special Rewards)
        }

        private static bool RewardFilter(RunState runState, RewardType reward, bool isBottomRoom)
        {
            if ((reward == RewardType.LegendaryArtifact || reward == RewardType.Artifact) && runState.artifactDeck.Count >= runState.campaign.maxArtifactCount)
                return false;
            if (reward == RewardType.Component && runState.cardDeck.Where(card => card.component == null).Count() < 3)
                return false;
            if (reward == RewardType.Special && (isBottomRoom || runState.remainingSpecialRewards == 0))
                return false;
            return true;
        }
    }
}
