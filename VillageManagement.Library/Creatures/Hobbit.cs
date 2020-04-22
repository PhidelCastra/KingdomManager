using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Hobbit : Creature
    {
        public Hobbit(string name, int age) 
            : base(name, age, CreatureType.Hobbit) 
        {
        }
    }
}
