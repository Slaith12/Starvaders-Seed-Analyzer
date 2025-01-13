using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvaders_Seed_Analyzer.Enums
{
    public enum RunMod
    {
        ChronoDifficulty,
        FixedDoom,
        FewerChoicesBool, //TODO: implement
        EchoDuringBossBool,
        CardDraw,
        EncounterModsBool, //TODO: implement
        BossLevel,
        AdvancedInvaders, //TODO: implement
        ShopPriceScaling,
        ActDoomHealBool,
        InvadersSpawnLowerBool,
        StartingMoney
    }

    public static class RunModValue
    {
        public enum ChronoDifficulty { ChronoHeal, Normal }
    }
}
