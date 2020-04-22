using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Ork : Creature
    {
        public Ork(string name, int age) 
            : base(name, age, CreatureType.Ork)
        {
        }
    }
}
