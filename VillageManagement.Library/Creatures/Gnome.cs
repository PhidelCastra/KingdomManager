using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Gnome : Creature
    {
        /* Constructor */
        public Gnome(string name, int age) 
            : base(name, age, CreatureType.Gnom)
        {
        }
    }
}
