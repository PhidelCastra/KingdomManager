using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Human : Creature
    {
        public Human(string name, int age) : 
            base(name, age, CreatureType.Mensch)
        {
        }
    }
}
