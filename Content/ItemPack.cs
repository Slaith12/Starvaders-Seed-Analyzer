namespace Starvaders_Seed_Analyzer.Content
{
    public class ItemPack(string name, List<CardInfo> cards, List<ArtifactInfo> artifacts)
    {
        public readonly string name = name;
        public readonly IReadOnlyList<CardInfo> cards = cards;
        public readonly IReadOnlyList<ArtifactInfo> artifacts = artifacts;
    }
}