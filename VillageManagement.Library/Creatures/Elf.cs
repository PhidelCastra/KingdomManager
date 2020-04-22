using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Elf : Creature
    {
        /* Constructor */
        public Elf(string name, int age) 
            : base(name, age, CreatureType.Elb)
        {
        }

        protected Elf(string name, int age, CreatureType type)
            : base(name, age, type)
        {

        }
    }
}
