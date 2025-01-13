using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    public class RunState
    {
        public readonly CampaignSettings campaign;
        public readonly int seed;
        public CharClassFlag character;

        public List<CardInstance> cardDeck;
        public List<ArtifactInfo> artifactDeck;

        public List<ItemPack> packs;
        public List<CardInfo> packCards => packs.Aggregate(new List<CardInfo>(), (list, pack) => list.Concat(pack.cards).ToList());
        public List<ArtifactInfo> packArtifacts => packs.Aggregate(new List<ArtifactInfo>(), (list, pack) => list.Concat(pack.artifacts).ToList());

        public int actNumber;
        public int dayNumber;
        public int chronoTokens;
        public int modifiedSeed => RNGHelper.GetModifiedSeed(seed, actNumber, dayNumber, chronoTokens);
        public float defaultRarityRatio => (float)actNumber / 4;

        public int remainingSpecialRewards;

        //Block list is not used in set seed runs; putting this here anyways in case I want to add random seed support in the future.
        public List<CardInfo> cardBlockList;
        public List<ArtifactInfo> artifactBlockList;
        public List<(CardInfo, ComponentInfo)> componentComboBlockList = new List<(CardInfo, ComponentInfo)>();
        private int blockListMax;

        public IEnumerable<CardInfo> GetAvailableCards() => campaign.cardList
            .Where(card => (card.classAvailability & character) != 0 
            || (card.classAvailability == CharClassFlag.Pack && packCards.Contains(card)));

        public IEnumerable<ArtifactInfo> GetAvailableArtifacts() => campaign.artifactList
            .Where(artifact => (artifact.classAvailability & character) != 0 
            || (artifact.classAvailability == CharClassFlag.Pack && packArtifacts.Contains(artifact)));

        public IEnumerable<ComponentInfo> GetAvailableComponents(CardInfo card) => campaign.componentList
            .Where(component => card.AllowsComponent(component) && (component.classAvailability & character) != 0);

        public int GetRunModValue(RunMod mod) => campaign.GetRunModValue(mod);

        public void AddToBlockList(CardInfo card) { }
        public void AddToBlockList(ArtifactInfo artifact) { }
        public void AddToBlockList(CardInfo card, ComponentInfo component) { }
        public void SetBlockList() { }
        public void ClearTempBlockList() { }

        public RunState(CampaignSettings campaign, int seed, CharClassFlag character, List<CardInstance> startingCards, List<ArtifactInfo> startingArtifacts, List<ItemPack> packs, int actNumber = 1, int dayNumber = 1, int chronoTokens = 3, int allowedSpecialRewards = 0, int blockListMax = 6)
        {
            this.campaign = campaign;
            this.seed = seed;
            this.character = character;
            this.cardDeck = new(startingCards);
            this.artifactDeck = new(startingArtifacts);
            this.packs = packs;

            this.actNumber = actNumber;
            this.dayNumber = dayNumber;
            this.chronoTokens = chronoTokens;

            remainingSpecialRewards = allowedSpecialRewards;
            
            cardBlockList = new List<CardInfo>();
            artifactBlockList = new List<ArtifactInfo>();
            componentComboBlockList = new List<(CardInfo, ComponentInfo)>();
            this.blockListMax = blockListMax;
        }
    }
}
