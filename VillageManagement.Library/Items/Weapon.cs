using System;
using System.Collections.Generic;
using System.Text;
using VillageManagement.Library.BaseItems;

namespace VillageManagement.Library.Items
{
    public class Weapon : Item
    {
        /// <summary>
        /// Power -value of the weapon.
        /// </summary>
        private int _magicPower;
        public int MagicPower { get => _magicPower; set
            {
                if(value < 0)
                {
                    throw new IndexOutOfRangeException($"Power value '{value}' is not valid.");
                }

                _magicPower = value;
            } 
        }

        /* Constructor */ 
        public Weapon(string weaponType, int power)
            : base(weaponType)
        {
            MagicPower = power;
        }
    }
}
