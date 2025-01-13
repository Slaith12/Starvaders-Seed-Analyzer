namespace Starvaders_Seed_Analyzer.Enums
{
    public enum CardTraitFlag
    {
        //Main traits
        AnyTrait = 0x1F,
        Attack = 0x1,
        MovePrimary = 0x2,
        Tactic = 0x4,
        Junk = 0x8,
        MoveSecondary = 0x10,
        Move = 0x12,
        //Name Traits
        FireName = 0x20,
        JabName = 0x40,
        BombName = 0x80,
        //Keyword/Mechanic traits
        Push = 0x100,
        Pull = 0x200,
        Summon = 0x400,
        Flow = 0x800,
        Shock = 0x1000,
        //Temporary Traits
        Burnt = 0x2000,
        Frozen = 0x4000,
    }
}
