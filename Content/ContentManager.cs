using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer.Content
{
    public class ContentManager
    {
        public readonly IReadOnlyList<CardInfo> cardList;
        public readonly IReadOnlyList<ArtifactInfo> artifactList;
        public readonly IReadOnlyList<ComponentInfo> componentList;
        public readonly IReadOnlyList<EnemyInfo> enemyList;
        public readonly IReadOnlyList<ItemPack> packList;

        public readonly IReadOnlyDictionary<string, CardInfo> cards;
        public readonly IReadOnlyDictionary<string, ArtifactInfo> artifacts;
        public readonly IReadOnlyDictionary<string, ComponentInfo> components;
        public readonly IReadOnlyDictionary<string, EnemyInfo> enemies;
        public readonly IReadOnlyDictionary<string, ItemPack> packs;

        public ContentManager()
        {
            cardList = new List<CardInfo>()
            {
                new("Shift", Rarity.Starter, CardTraitFlag.MovePrimary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentBlacklistOverride: ["Boosted", "Risky", "Wild"]),

                new("Fire!", Rarity.Starter, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Boosted", "Fire!", "Risky", "Chain"]),

                new("Nova Bomb", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Prime", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.Push, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Swift"]),

                new("Quickdraw", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed),

                new("Pusher", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.Push, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push),

                new("Afterburn", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Repeated),

                new("Jam", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Triple Fire!", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Bullet | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!"]),

                new("Momentum", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed),
                
                new("BackFire!", Rarity.Legendary, CardTraitFlag.Attack | CardTraitFlag.MoveSecondary | CardTraitFlag.FireName, CharClassFlag.Gunner, //ok run and gun is actually a good name but AAAAAHHHH
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet,
                componentBlacklistOverride: ["Wild", "Fire!", "Breezy"]),
                
                new("Shimmer", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Artillery Strike", Rarity.Common, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Fire EVERYTHING!", Rarity.Legendary, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed,
                componentBlacklistOverride: ["Fire!"]),

                new("Cross Fire!", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Star"],
                componentBlacklistOverride: ["Fire!", "Breezy"]),

                new("Flamethrower", Rarity.Common, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack,
                componentTraitBlacklist: ComponentTraitFlag.Repeated,
                componentBlacklistOverride: ["Fire!"]),
                
                new("Repair", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated),

                new("Concussive Blast", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.Push, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Push,
                componentWhitelistOverride: ["Boomer"]),

                new("Aegis Bomb", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Swap", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Returnal", Rarity.Rare, CardTraitFlag.MovePrimary, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Exhaust", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.CardManip,
                componentBlacklistOverride: ["Wild"]),

                new("Throw Bomb", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Push | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated,
                componentBlacklistOverride: ["Fire!"]),

                new("Big Bang!", Rarity.Legendary, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated),

                new("Remote", Rarity.Common, CardTraitFlag.MovePrimary | CardTraitFlag.Push, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push,
                componentTraitBlacklist: ComponentTraitFlag.Repeated),

                new("Explosive Shield", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical"]),
                
                new("Cluster Fire!", Rarity.Rare, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!"]),

                new("Scorching Sun", Rarity.Rare, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack,
                componentWhitelistOverride: ["Swift"]),

                new("Into the Fray", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Repeated),
                
                new("Plasma Fire!", Rarity.Rare, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!", "Piercing", "Breezy"]),

                new("Fuel", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.Junk, CharClassFlag.Roxy,
                componentWhitelist: ["Fire!", "Swift", "Boomer"]),
                
                new("Salve", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated),
                
                new("Dodge", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Fire!", "Jab"]),
                
                new("Augment", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.CardManip | ComponentTraitFlag.Costed,
                componentWhitelistOverride: ["Brainiac"]),
                
                new("Repulsion", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.Push, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess | ComponentTraitFlag.Push,
                componentTraitBlacklist: ComponentTraitFlag.Selection),

                new("Eject", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.Repeated | ComponentTraitFlag.CardManip | ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical+", "Swift+"],
                componentBlacklistOverride: ["Chilled", "Broken", "Echo", "Linked", "Tactical", "Swift"]),

                new("Siphon Fire!", Rarity.Rare, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!"]),

                new("Corruption", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Breach", Rarity.Junk, CardTraitFlag.Junk | CardTraitFlag.Push, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Mangled", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Hallucination", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("Quick Fire!", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!"]),

                new("MisFire!", Rarity.Rare, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Bullet | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed,
                componentBlacklistOverride: ["Breezy"]),

                new("Reactor Burn", Rarity.Rare, CardTraitFlag.Attack | CardTraitFlag.MoveSecondary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack,
                componentBlacklistOverride: ["Wild"]),

                new("Meltdown", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Gunner,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"]),

                new("HELL Fire!", Rarity.Legendary, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!", "Chilled", "Broken"]),

                new("Sparks", Rarity.Created, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical", "Swift", "Piercing"]),

                new("Rocket Fist", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.Push, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection),

                new("Tractor Beam", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.MoveSecondary | CardTraitFlag.Push, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push),

                new("Warp", Rarity.Created, CardTraitFlag.MovePrimary, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated | ComponentTraitFlag.NonPurge),

                new("Blaze", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Roxy,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.CardManip | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical", "Chain"]),

                new("Copycat", Rarity.Legendary, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.CardManip,
                componentWhitelistOverride: ["Tactical+"],
                componentBlacklistOverride: ["Tactical"]),

                new("Lock N Load", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed),

                new("Incinerate", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.Repeated | ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical+", "Swift+"],
                componentBlacklistOverride: ["Tactical", "Swift"]),

                new("Overclock", Rarity.Legendary, CardTraitFlag.Tactic, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical"]),

                new("Bomb Rain", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess | ComponentTraitFlag.Costed,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Rainbow"]),

                new("Breakdown", Rarity.Rare, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical"]),

                new("Tornado!", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.Push, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical+", "Swift+"],
                componentBlacklistOverride: ["Tactical", "Swift"]),

                new("Titan Rocket", Rarity.Rare, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Tactical+"],
                componentBlacklistOverride: ["Tactical"]),

                new("Detonator", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical"]),

                new("SCR-4P", Rarity.Legendary, CardTraitFlag.Junk | CardTraitFlag.Attack, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical"]),

                new("Plasma Bomb", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.BombName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Decoy Bombs", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Repeated,
                componentBlacklistOverride: ["Tactical"]),
                
                new("Annihilation", Rarity.Legendary, CardTraitFlag.Attack, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack,
                componentTraitBlacklist: ComponentTraitFlag.Repeated | ComponentTraitFlag.CardManip),
                
                new("Rush", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated),

                new("Burst Fire!", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.FireName | CardTraitFlag.Push, CharClassFlag.Gunner, //...trigger fire!?!?
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Bullet | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!", "Swift"]),

                new("Ricochet Fire!", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Fire!", "Piercing", "Breezy"]),

                new("Icicle", Rarity.Common, CardTraitFlag.Attack, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack,
                componentTraitBlacklist: ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Swift"],
                componentBlacklistOverride: ["Frosty"]),

                new("Hyperfreeze", Rarity.Legendary, CardTraitFlag.Tactic, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.CardManip),

                new("Say Hello to...", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.Summon, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Costed),
                
                new("Propel", Rarity.Created, CardTraitFlag.MovePrimary | CardTraitFlag.Pull, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Tactical"]),

                new("Vapor", Rarity.Created, CardTraitFlag.Tactic, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge | ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical", "Fire!"]),

                new("Novalanche", Rarity.Starter, CardTraitFlag.Tactic | CardTraitFlag.Summon, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Repeated),

                new("Flurry Fire!", Rarity.Starter, CardTraitFlag.Attack | CardTraitFlag.FireName, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Attack | ComponentTraitFlag.Bullet | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Boosted", "Fire!", "Breezy", "Broken", "Chilled", "Risky"]),

                new("Tinker", Rarity.Starter, CardTraitFlag.Tactic, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.CardManip,
                componentWhitelistOverride: ["Wild"]),
                
                new("Reckoning", Rarity.Created, CardTraitFlag.Attack, CharClassFlag.Roxy,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentBlacklistOverride: ["Broken"]),
                
                new("Galeforce", Rarity.Common, CardTraitFlag.Move | CardTraitFlag.Push, CharClassFlag.Pack, //Sonic Blitz is a pretty different name
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push,
                componentBlacklistOverride: ["Boosted", "Wild"]),
                
                new("Liquid Coolant", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.Junk, CharClassFlag.Gunner,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated | ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Frosty"]),

                new("Cold Snap", Rarity.Rare, CardTraitFlag.Attack, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection),

                new("Chisel", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.CardManip,
                componentWhitelistOverride: ["Tactical", "Wild"]),

                new("Snowbomb!", Rarity.Common, CardTraitFlag.Tactic | CardTraitFlag.BombName | CardTraitFlag.Push | CardTraitFlag.Summon, CharClassFlag.Noel,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Push | ComponentTraitFlag.Attack | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated),

                new("Trailblazer", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Roxy,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Frosty"]),

                new("Slow Burn", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Roxy,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated | ComponentTraitFlag.Costed),

                new("Smelt", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Tactical"]),

                new("Theorycraft", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Swift+"],
                componentBlacklistOverride: ["Swift"]),

                new("Fabricate", Rarity.Common, CardTraitFlag.Tactic, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed),

                new("Survey", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Prototype", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.Summon, CharClassFlag.Zeke,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Tactical+"],
                componentBlacklistOverride: ["Tactical"]),

                new("Ready Rocket", Rarity.Legendary, CardTraitFlag.Tactic, CharClassFlag.Roxy,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection,
                componentWhitelistOverride: ["Tactical+", "Swift+"],
                componentBlacklistOverride: ["Tactical", "Swift", "Fire!"]),

                new("Backtrack", Rarity.Common, CardTraitFlag.MovePrimary, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Repeated),
                
                new("Dark Power", Rarity.Created, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.NonPurge | ComponentTraitFlag.CardManip,
                componentWhitelistOverride: ["Wild"]),

                new("Decimate", Rarity.Legendary, CardTraitFlag.Tactic | CardTraitFlag.Summon, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed),
                
                new("Gust Down", Rarity.Created, CardTraitFlag.Tactic | CardTraitFlag.Pull, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge),

                new("Gust Up", Rarity.Created, CardTraitFlag.Tactic | CardTraitFlag.Pull, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge),

                new("Gust Left", Rarity.Created, CardTraitFlag.Tactic | CardTraitFlag.Pull, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge),

                new("Gust Right", Rarity.Created, CardTraitFlag.Tactic | CardTraitFlag.Pull, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge),
                
                new("Reload", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Selection),

                new("Reverb", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.NonPurge | ComponentTraitFlag.Costed),
                
                new("Boulder Toss", Rarity.Common, CardTraitFlag.Attack | CardTraitFlag.Summon, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic),

                new("Cold Gust", Rarity.Starter, CardTraitFlag.Tactic | CardTraitFlag.Pull, CharClassFlag.Noel, //why was this called "Waft"? ._.
                componentTraitWhitelist: ComponentTraitFlag.Basic,
                componentTraitBlacklist: ComponentTraitFlag.Costed),

                //I intentionally skip over the sections obviously meant for other classes so I don't leak extra cards, and my reward is missing 3 neutral cards (including a pack card???) placed in the stinger section...
                new("Refresh", Rarity.Rare, CardTraitFlag.Tactic, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.SelectionLess,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Costed | ComponentTraitFlag.Repeated | ComponentTraitFlag.NonPurge,
                componentWhitelistOverride: ["Tactical+", "Swift+"],
                componentBlacklistOverride: ["Tactical", "Swift"]),

                new("Tempo", Rarity.Rare, CardTraitFlag.MovePrimary, CharClassFlag.Neutral,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Move,
                componentTraitBlacklist: ComponentTraitFlag.Selection | ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Jab", "Fire!"]),

                new("Reposition", Rarity.Rare, CardTraitFlag.Tactic | CardTraitFlag.MoveSecondary, CharClassFlag.Pack,
                componentTraitWhitelist: ComponentTraitFlag.Basic | ComponentTraitFlag.Move,
                componentTraitBlacklist: ComponentTraitFlag.Repeated,
                componentWhitelistOverride: ["Jab", "Fire!"]),
                
                new("Check", Rarity.Junk, CardTraitFlag.Junk, CharClassFlag.Neutral,
                componentWhitelist: ["Fire!", "Swift", "Tactical", "Chilled"])
            };
            Dictionary<string, CardInfo> cards = new();
            foreach (var card in cardList)
            {
                cards.Add(card.name, card);
                cards.Add(card.name.Replace(" ", "").Replace("-", "").Replace("!", "").ToLower(), card);
            }
            this.cards = cards;

            artifactList = new List<ArtifactInfo>()
            {
                new("Nuclear Reactor", Rarity.Legendary, CharClassFlag.Zeke),
                new("Hot Head", Rarity.Legendary, CharClassFlag.Gunner),
                new("Incinerator", Rarity.Legendary, CharClassFlag.Roxy),
                new("Quantum Burn", Rarity.Common, CharClassFlag.Gunner),
                new("Threat Radar", Rarity.Common, CharClassFlag.Gunner),
                new("Holtzman Shields", Rarity.Legendary, CharClassFlag.Gunner),
                new("Thermal Beam", Rarity.Common, CharClassFlag.Gunner),
                new("AntiMag", Rarity.Common, CharClassFlag.Pack),
                new("Jester", Rarity.Legendary, CharClassFlag.Gunner),
                new("Heat Waste", Rarity.Legendary, CharClassFlag.Gunner),
                new("Fire Starter", Rarity.Starter, CharClassFlag.Roxy),
                new("Laser Bullets", Rarity.Legendary, CharClassFlag.Gunner),
                new("Shrapnel", Rarity.Common, CharClassFlag.Gunner),
                new("Mecha OS", Rarity.Legendary, CharClassFlag.Gunner),
                new("Haywire", Rarity.Common, CharClassFlag.Neutral),
                new("Insulation", Rarity.Common, CharClassFlag.Gunner),
                new("Flash Fire", Rarity.Legendary, CharClassFlag.Gunner),
                new("Balloomb", Rarity.Legendary, CharClassFlag.Gunner),
                new("Red Giant", Rarity.Legendary, CharClassFlag.Gunner),
                new("Starter Fluid", Rarity.Legendary, CharClassFlag.Neutral),
                new("Repeater", Rarity.Legendary, CharClassFlag.Pack),
                new("Bomber Mech", Rarity.Common, CharClassFlag.Gunner),
                new("Nova Rays", Rarity.Legendary, CharClassFlag.Gunner),
                new("Supply Drop", Rarity.Common, CharClassFlag.Gunner),
                new("Mine Field", Rarity.Common, CharClassFlag.Gunner),
                new("Wormhole", Rarity.Common, CharClassFlag.Neutral),
                new("Big Fridge", Rarity.Legendary, CharClassFlag.Gunner),
                new("Furnace", Rarity.Legendary, CharClassFlag.Gunner),
                new("The Machine", Rarity.Starter, CharClassFlag.Zeke),
                new("Perma-Frost", Rarity.Starter, CharClassFlag.Noel),
                new("Robo Parrot", Rarity.Common, CharClassFlag.Pack),
                new("Tracer Rounds", Rarity.Legendary, CharClassFlag.Gunner),
                new("Retro Specters", Rarity.Legendary, CharClassFlag.Neutral),
                new("Wanderer", Rarity.Legendary, CharClassFlag.Neutral),
                new("Engi-89", Rarity.Legendary, CharClassFlag.Neutral),
                new("Sonic BOOM", Rarity.Legendary, CharClassFlag.Gunner),
                new("Greenhouse", Rarity.Legendary, CharClassFlag.Roxy),
                new("Cold Star", Rarity.Legendary, CharClassFlag.Noel),
                new("Iceberg", Rarity.Legendary, CharClassFlag.Noel),
                new("Fi-Tech", Rarity.Legendary, CharClassFlag.Zeke),
                new("Blueprint", Rarity.Common, CharClassFlag.Neutral),
                new("Volcanic Ash", Rarity.Common, CharClassFlag.Gunner),
                new("Mobius Gem", Rarity.Special, CharClassFlag.Neutral),
                new("Nemesis", Rarity.Common, CharClassFlag.Neutral),
                new("Multi-Pass", Rarity.Common, CharClassFlag.Neutral),
                new("Peashooter", Rarity.Common, CharClassFlag.Pack),
                new("Gift Card", Rarity.Common, CharClassFlag.Neutral),
                new("Bandages", Rarity.Common, CharClassFlag.Neutral),
                new("Moonlit Ritual", Rarity.Legendary, CharClassFlag.Neutral),
                new("Wind God", Rarity.Legendary, CharClassFlag.Pack),
                new("Storm Chaser", Rarity.Common, CharClassFlag.Pack),
                new("Rock", Rarity.Junk, CharClassFlag.Neutral),
                new("Lizard Soul", Rarity.Common, CharClassFlag.Neutral),
                new("Mystery Bear", Rarity.Common, CharClassFlag.Neutral),
                new("Interlinked", Rarity.Common, CharClassFlag.Neutral),
                new("Cosmic Dust", Rarity.Special, CharClassFlag.Neutral),
                new("Cherubim", Rarity.Special, CharClassFlag.Neutral),
                new("Gemini", Rarity.Special, CharClassFlag.Neutral),
            };
            Dictionary<string, ArtifactInfo> artifacts = new();
            foreach (var artifact in artifactList)
            {
                artifacts.Add(artifact.name, artifact);
                artifacts.Add(artifact.name.Replace(" ", "").Replace("!", "").ToLower(), artifact);
            }
            this.artifacts = artifacts;

            componentList = new List<ComponentInfo>()
            {
                new("Echo", CharClassFlag.Neutral, ComponentTraitFlag.Basic | ComponentTraitFlag.Repeated | ComponentTraitFlag.NonPurge),
                new("Piercing", CharClassFlag.Gunner, ComponentTraitFlag.Bullet),
                new("Chilled", CharClassFlag.Gunner, ComponentTraitFlag.Basic | ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge),
                new("Zephyr", CharClassFlag.Neutral, ComponentTraitFlag.Push),
                new("Star", CharClassFlag.Gunner, 0),
                new("Fire!", CharClassFlag.Gunner, ComponentTraitFlag.Basic | ComponentTraitFlag.NonPurge, CardTraitFlag.Attack | CardTraitFlag.FireName),
                new("Broken", CharClassFlag.Gunner, ComponentTraitFlag.Basic | ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge, CardTraitFlag.Junk),
                new("Swift", CharClassFlag.Neutral, ComponentTraitFlag.SelectionLess, CardTraitFlag.MoveSecondary),
                new("Boosted", CharClassFlag.Neutral, ComponentTraitFlag.Basic | ComponentTraitFlag.Repeated),
                new("Boomer", CharClassFlag.Gunner, 0),
                new("Chain", CharClassFlag.Neutral, ComponentTraitFlag.Attack | ComponentTraitFlag.NonPurge),
                new("Tactical", CharClassFlag.Neutral, ComponentTraitFlag.Basic | ComponentTraitFlag.Costed, CardTraitFlag.Tactic),
                new("Frosty", CharClassFlag.Gunner, ComponentTraitFlag.Basic | ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge, CardTraitFlag.Tactic),
                new("Rainbow", CharClassFlag.Gunner, 0),
                new("Brainiac", CharClassFlag.Gunner, 0),
                new("Linked", CharClassFlag.Neutral, ComponentTraitFlag.Basic | ComponentTraitFlag.NonPurge, 0, Rarity.Created),
                new("Heated", CharClassFlag.Gunner, ComponentTraitFlag.Basic | ComponentTraitFlag.Costed | ComponentTraitFlag.NonPurge, CardTraitFlag.Junk, Rarity.Junk),
                new("Parrot", CharClassFlag.Neutral, 0),
                new("Risky", CharClassFlag.Neutral, ComponentTraitFlag.Basic | ComponentTraitFlag.Repeated),
                new("Breezy", CharClassFlag.Gunner, ComponentTraitFlag.Bullet),
                new("Wild", CharClassFlag.Neutral, ComponentTraitFlag.Repeated | ComponentTraitFlag.Selection),
                new("Theory", CharClassFlag.Neutral, 0, CardTraitFlag.Junk, Rarity.Created),
                new("Tactical+", CharClassFlag.Neutral, 0, CardTraitFlag.Tactic),
                new("Swift+", CharClassFlag.Neutral, 0, CardTraitFlag.MoveSecondary),
                new("Virus", CharClassFlag.Neutral, 0, CardTraitFlag.Junk, Rarity.Junk),
            };
            Dictionary<string, ComponentInfo> components = new();
            foreach (var component in componentList)
            {
                components.Add(component.name, component);
                components.Add(component.name.Replace(" ", "").Replace("!", "").ToLower(), component);
            }
            this.components = components;

            enemyList = new List<EnemyInfo>()
            {
                new("Migo", 1),
                new("Shooter", 1),
                new("Blob", 1),
                new("Mini-Blob", 1),
                new("Mega Blob", 0),
                new("Leviathan", 2),
                //I got lazy and stopped adding star counts here
                new("Leviathan (Body)"),
                new("Ghost"),
                new("Airship"),
                new("Swarmer"),
                new("Rocketeer"),
                new("Rusher"),
                new("Orchid"),
                new("Broodmother"),
                new("Tick"),
                new("Mothership"),
                new("Shelled"),
                new("Pulsar"),
                new("Astromancer"),
                new("Kraken Core"),
                new("Kraken Wall"),
                new("Kraken Tentacle"),
                new("Kraken Meat"),
                new("Mimic"),
                new("Time Angel"),
                new("Spitter"),
                new("Airship+"),
                new("Shooter+"),
                new("Rocketeer+"),
                new("Voidspawn"),
                new("Asteroid"),
                new("Vengoid"),
                new("Shambler"),
                new("Migo+"),
                new("Swarmer+"),
                new("Vengoid+"),
                new("Blob+"),
                new("Shelled+"),
                new("Broodmother+"),
                new("Tick+"),
                new("Spitter+"),
                new("Orchid+"),
                new("Rusher+"),
                new("Leviathan+"),
                new("Leviathan+ (Body)"),
                new("Voidspawn+"),
                new("Mimic+"),
                new("Pulsar+"),
                new("Astromancer+"),
                new("Ghost+"),
                new("Boomer"),
                new("Boomer+"),
                new("Virus"),
                new("Virus+"),
                new("Fallen Angel"),
                new("Fallen Angel+"),
                new("Echo"),
                new("Weaver"),
                new("Weaver+"),
                new("Grandmaster"),
                new("Pawn"),
                new("Rook"),
                new("Knight"),
                new("Bishop"),
                new("Queen"),
            };
            Dictionary<string, EnemyInfo> enemies = new();
            foreach (var enemy in enemyList)
            {
                enemies.Add(enemy.name, enemy);
                enemies.Add(enemy.name.Replace(" ", "").Replace("!", "").ToLower(), enemy);
            }
            this.enemies = enemies;

            packList = new List<ItemPack>()
            {
                new("Tempest",
                [cards["Pusher"], cards["Concussive Blast"], cards["Tractor Beam"], cards["Tornado!"], cards["Galeforce"]],
                [artifacts["Storm Chaser"], artifacts["Wind God"], artifacts["AntiMag"]]),

                new("Repeater",
                [cards["Prime"], cards["Reload"], cards["Decimate"], cards["Reverb"], cards["Reposition"]],
                [artifacts["Peashooter"], artifacts["Repeater"], artifacts["Robo Parrot"]])
            };
            Dictionary<string, ItemPack> packs = new();
            foreach (var pack in packList)
            {
                packs.Add(pack.name, pack);
            }
            this.packs = packs;
        }
    }
}
