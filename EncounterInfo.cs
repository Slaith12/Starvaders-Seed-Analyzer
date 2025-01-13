using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public class EncounterInfo(EncounterTemplate template, EncounterTitle title, List<EnemyInfo> enemyList, RewardType rewardType, bool deductsDoom = false)
    {
        public readonly EncounterTemplate template = template;
        public readonly EncounterTitle title = title;
        public readonly IReadOnlyList<EnemyInfo> enemyList = enemyList;
        public readonly RewardType rewardType = rewardType;
        public bool deductsDoom = deductsDoom;

        public EncounterType type => template.type;
        public string titleString => title.ToString(type, enemyList[0].name);

        public override string ToString()
        {
            return $"{titleString}\n{type} encounter with {string.Join(", ", enemyList.Distinct())}.\nReward: {rewardType}" + (deductsDoom ? " and -1 doom." : ".");
        }
    }
}
