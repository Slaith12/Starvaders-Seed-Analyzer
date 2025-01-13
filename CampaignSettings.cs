using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public class CampaignSettings(ContentManager content, CampaignRoomGenerator roomGenerator, Dictionary<RunMod, int>? runMods = null, IReadOnlySet<CardInfo>? excludedCards = null, IReadOnlySet<ArtifactInfo>? excludedArtifacts = null, IReadOnlySet<EnemyInfo>? excludedEnemies = null, int maxArtifactCount = 27)
    {
        public static readonly IReadOnlyDictionary<RunMod, int> defaultRunMods;
        public readonly ContentManager content = content;
        public readonly CampaignRoomGenerator roomGenerator = roomGenerator;
        public readonly IReadOnlySet<CardInfo> excludedCards = excludedCards ?? new HashSet<CardInfo>();
        public readonly IReadOnlySet<ArtifactInfo> excludedArtifacts = excludedArtifacts ?? new HashSet<ArtifactInfo>();
        public readonly IReadOnlySet<EnemyInfo> excludedEnemies = excludedEnemies ?? new HashSet<EnemyInfo>();
        private readonly IReadOnlyDictionary<RunMod, int> runMods = runMods ?? new Dictionary<RunMod, int>();
        public readonly int maxArtifactCount = maxArtifactCount;

        public IEnumerable<CardInfo> cardList => content.cardList.Where(card => !excludedCards.Contains(card));
        public IEnumerable<ArtifactInfo> artifactList => content.artifactList.Where(artifact => !excludedArtifacts.Contains(artifact));
        public IEnumerable<ComponentInfo> componentList => content.componentList;
        public IEnumerable<EnemyInfo> enemyList => content.enemyList.Where(enemy => !excludedEnemies.Contains(enemy));

        public IReadOnlyDictionary<string, CardInfo> cards => content.cards;
        public IReadOnlyDictionary<string, ArtifactInfo> artifacts => content.artifacts;
        public IReadOnlyDictionary<string, ComponentInfo> components => content.components;
        public IReadOnlyDictionary<string, EnemyInfo> enemies => content.enemies;

        public int GetRunModValue(RunMod mod)
        {
            return runMods.GetValueOrDefault(mod, defaultRunMods[mod]);
        }

        public List<EncounterTemplate> GetRoomTemplates(int actNumber, int dayNumber, int roomAmount, Random rng) => roomGenerator.GetRoomTemplates(actNumber, dayNumber, roomAmount, rng);

        public List<(EnemyInfo, EnemyInfo)> GetImpossibleEnemyCombinations()
        {
            return new List<(EnemyInfo, EnemyInfo)>() { (enemies["Leviathan"], enemies["Mega Blob"]) };
        }

        static CampaignSettings()
        {
            defaultRunMods = new Dictionary<RunMod, int>()
            {
                { RunMod.ChronoDifficulty, (int)RunModValue.ChronoDifficulty.ChronoHeal },
                { RunMod.FixedDoom, 0 },
                { RunMod.FewerChoicesBool, 0 },
                { RunMod.EchoDuringBossBool, 0 },
                { RunMod.CardDraw, 5 },
                { RunMod.EncounterModsBool, 0 },
                { RunMod.BossLevel, 2 },
                { RunMod.AdvancedInvaders, 1 },
                { RunMod.ShopPriceScaling, 10 },
                { RunMod.ActDoomHealBool, 0 },
                { RunMod.InvadersSpawnLowerBool, 0 },
                { RunMod.StartingMoney, 0 },
            };
        }
    }
}
