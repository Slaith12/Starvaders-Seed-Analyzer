using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starvaders_Seed_Analyzer.Content
{
    public class EnemyInfo(string name, int baseStars = 1)
    {
        public string name = name;
        public int baseStars = baseStars;

        public override string ToString()
        {
            return name;
        }
    }
}
