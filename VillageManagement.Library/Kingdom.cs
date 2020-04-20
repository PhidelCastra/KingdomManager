using System;
using System.Collections.Generic;
using System.Text;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Items;
using VillageManagement.Library.Tribes;

namespace VillageManagement.Library
{
    public class Kingdom
    {   
        // Just for prevent loading for some functions of this class.
        private bool _loaded;
        
        /// <summary>
        /// Class to create items for creatures.
        /// </summary>
        private ItemHandler _itemHandler;

        /// <summary>
        /// List of all prepared tribes.
        /// </summary>
        public List<Tribe<Creature>> Tribes { get; private set; }

        /* Constructor */
        public Kingdom()
        {
            Tribes = new List<Tribe<Creature>>();

            _itemHandler = new ItemHandler();
        }

        /// <summary>
        /// Loads preconditions (first tribes). 
        /// </summary>
        /// <returns>True if load state was execute first time, otherwise false.</returns>
        public bool LoadPreconditions()
        {
            if (_loaded)
            {
                return false;
            }

            LoadDwarfTribes();
            LoadElbTribes();

            return true;
        }

        /// <summary>
        /// Prepare some Tribes for dwarf type.
        /// </summary>
        private void LoadDwarfTribes()
        {
            var axt1 = _itemHandler.CreateAxt(12);
            var axt2 = _itemHandler.CreateAxt(17);
            var sword1 = _itemHandler.CreateSword(15);
            var quarrelingHammer1 = _itemHandler.CreateQarrelingHammer(15);
            var magicStick1 = _itemHandler.CreateMagicStick(45);

            var dwarf1 = new Dwarf("Gimli", 140);
            var dwarf2 = new Dwarf("Zwingli", 70);
            var dwarf3 = new Dwarf("Gumli", 163);

            dwarf1.AddItem(axt1);
            dwarf1.AddItem(sword1);

            dwarf2.AddItem(axt2);

            dwarf3.AddItem(magicStick1);
            dwarf3.AddItem(quarrelingHammer1);

            var tribe1 = new Tribe<Creature>("Altobarden", 1247, CreatureType.Zwerg);
            var tribe2 = new Tribe<Creature>("Elbknechte", 1023, CreatureType.Zwerg);

            tribe1.AddTribesman(dwarf1);
            tribe1.AddTribesman(dwarf2);
            tribe1.Chief = dwarf1;

            tribe2.AddTribesman(dwarf3);

            Tribes.Add(tribe1);
            Tribes.Add(tribe2);

            _loaded = true;
        }

        /// <summary>
        /// Prepare some tribes for elfes.<
        /// </summary>
        private void LoadElbTribes()
        {
            var bow1 = _itemHandler.CreateBow(14);
            var bow2 = _itemHandler.CreateBow(14);
            var bow3 = _itemHandler.CreateBow(14);
            var bow4 = _itemHandler.CreateBow(14);
            var bow5 = _itemHandler.CreateBow(14);
            var bow6 = _itemHandler.CreateBow(14);
            var bow7 = _itemHandler.CreateBow(14);

            var magicStick1 = _itemHandler.CreateMagicStick(67);
            var magicStick2 = _itemHandler.CreateMagicStick(80);

            var sword1 = _itemHandler.CreateSword(20);
            var sword2 = _itemHandler.CreateSword(20);

            var knife1 = _itemHandler.CreatKnife(11);
            var knife2 = _itemHandler.CreatKnife(11);
            var knife3 = _itemHandler.CreatKnife(11);
            var knife4 = _itemHandler.CreatKnife(11);

            var elf1 = new Elf("Gil-galad", 205);
            var elf2 = new Elf("Teleri", 121);
            var elf3 = new Elf("Finwe", 364);
            var elf4 = new Elf("Feanor", 98);
            var elf5 = new Elf("Fingolfin", 154);
            var elf6 = new Elf("Turgon", 242);
            var elf7 = new Elf("Celeborn", 769);
            var elf8 = new Elf("Glorfindel", 493);
            var elf9 = new Elf("Luthien", 1632);

            elf1.AddItem(bow1);
            elf1.AddItem(sword1);
            elf2.AddItem(bow2);
            elf2.AddItem(knife1);
            elf3.AddItem(magicStick1);
            elf4.AddItem(bow3);
            elf5.AddItem(sword2);
            elf5.AddItem(knife4);
            elf6.AddItem(bow4);
            elf7.AddItem(bow5);
            elf7.AddItem(knife2);
            elf8.AddItem(bow7);
            elf8.AddItem(knife3);
            elf9.AddItem(magicStick2);


            var tribe1 = new Tribe<Creature>("Vanyar", 34, CreatureType.Elb);
            var tribe2 = new Tribe<Creature>("Noldor", 145, CreatureType.Elb);

            tribe1.AddTribesman(elf9);
            tribe1.Chief = elf9;
            tribe1.AddTribesman(elf1);
            tribe1.AddTribesman(elf2);
            tribe1.AddTribesman(elf4);

            tribe2.AddTribesman(elf3);
            tribe2.Chief = elf3;
            tribe2.AddTribesman(elf5);
            tribe2.AddTribesman(elf6);
            tribe2.AddTribesman(elf7);
            tribe2.AddTribesman(elf8);

            Tribes.Add(tribe1);
            Tribes.Add(tribe2);
        }
    }
}
