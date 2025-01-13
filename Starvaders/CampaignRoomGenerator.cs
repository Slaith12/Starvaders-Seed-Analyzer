using Starvaders_Seed_Analyzer.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvaders_Seed_Analyzer
{
    public class CampaignRoomGenerator(List<CampaignRoomGenerator.GenData>[] actRoomGenerators)
    {
        private List<GenData>[] actRoomGenerators = actRoomGenerators;

        public List<EncounterTemplate> GetRoomTemplates(int actNumber, int dayNumber, int roomAmount, Random rng)
        {
            if (actNumber == 1)
                dayNumber--;
            GenData generator = actRoomGenerators[actNumber - 1][dayNumber - 1];
            roomAmount = Math.Min(roomAmount, generator.maxRoomAmount);
            return generator.templateWeights
                .PickRandomWeighted(roomWeightData => roomWeightData.Item2, roomAmount, rng)
                .Select(roomWeightData => roomWeightData.Item1).ToList();
        }

        public struct GenData(List<(EncounterTemplate, int)> templateWeights, int maxRoomAmount = 3)
        {
            public List<(EncounterTemplate, int)> templateWeights = templateWeights;
            public int maxRoomAmount = maxRoomAmount;
        }
    }

}
