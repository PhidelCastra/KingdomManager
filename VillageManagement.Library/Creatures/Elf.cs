using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Creatures
{
    public class Elf : Creature
    {
        /// <summary>
        /// Hair length property.
        /// </summary>
        private float _hairLength;
        public float HairLength { get => _hairLength; set {
                if (value < 0)
                    throw new NotSupportedException("Hair length must be longer than 0.");
                _hairLength = value;
            }
        }

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
