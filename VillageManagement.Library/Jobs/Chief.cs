using System;
using System.Collections.Generic;
using System.Text;
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Interfaces;

namespace VillageManagement.Library.Jobs
{
    public class Chief : Creature, IChief
    {
        private readonly int _chiefSince;
        public int ChiefSince => _chiefSince;

        /* Constructor */
        public Chief(Creature creature, int chiefSince) 
            : base(creature.Name, creature.Age, creature.CreatureType)
        {
            _chiefSince = chiefSince;

            creature.Items.GetWeapons().ForEach(w => Items.Add(w));
        }
    }
}
