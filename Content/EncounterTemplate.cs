using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer.Content
{
    public class EncounterTemplate(string label, int enemyAmount, int difficulty, List<(EnemyInfo, int)> enemyCountData, List<(RewardType, float)> rewardWeights, int gridWidth, int gridHeight, int maxTurns = -1, EncounterType type = EncounterType.Normal)
    {
        public readonly string label = label;
        public readonly int gridWidth = gridWidth;
        public readonly int gridHeight = gridHeight;
        public readonly int maxTurns = maxTurns;
        public readonly EncounterType type = type;
        public readonly IReadOnlyList<(EnemyInfo, int)> enemyCountData = enemyCountData;
        public readonly int enemyAmount = enemyAmount;
        public readonly int difficulty = difficulty;
        public readonly IReadOnlyList<(RewardType, float)> rewardWeights = rewardWeights;
    }
}
