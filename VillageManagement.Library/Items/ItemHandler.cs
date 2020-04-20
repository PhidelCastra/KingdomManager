using System;
using System.Collections.Generic;
using System.Text;
using VillageManagement.Library.BaseItems;

namespace VillageManagement.Library.Items
{
    /// <summary>
    /// Create the different weapons with methods of this class.
    /// </summary>
    public class ItemHandler
    {
        public Weapon CreateAxt(int power)
        {
            return CreateWeapon("Axt", power);
        }

        public Weapon CreateSword(int power)
        {
            return CreateWeapon("Sword", power);
        }

        public Weapon CreateMagicStick(int power)
        {
            return CreateWeapon("Magic stick", power);
        }

        public Weapon CreateQarrelingHammer(int power)
        {
            return CreateWeapon("Qarreling hammer", power);
        }

        public Weapon CreateBow(int power)
        {
            return CreateWeapon("Bow", power);
        }

        public Weapon CreatKnife(int power)
        {
            return CreateWeapon("Knife", power);
        }

        public Weapon CreateSpear(int power)
        {
            return CreateWeapon("Spear", power);
        }

        private Weapon CreateWeapon(string name, int power)
        {
            return new Weapon(name, power);
        }
    }
}
