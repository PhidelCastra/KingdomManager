using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class HighElf : Elf
    {
        /* Constructor */
        public HighElf(string name, int age) 
            : base(name, age, CreatureType.Hochelf)
        {
        }
    }
}
