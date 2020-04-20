using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Items;

namespace VillageManagement.Library.Creatures
{
    public abstract class Creature
    {
        /// <summary>
        /// Counts the created creatures and set as ID. Should be increments by one after each instantiating.
        /// </summary>
        private static int _creatureCount;

        /// <summary>
        /// Default max count of the inventory list.
        /// </summary>
        private int _defaultMaxCount = 5;

        /// <summary>
        /// Creature type of the instance.
        /// </summary>
        public CreatureType CreatureType { get; private set; }

        /// <summary>
        /// True if this creature is set by Chief position in a tribe.
        /// </summary>
        public bool IsChief { get; set; }

        /// <summary>
        /// Unique ID of this creature.
        /// </summary>
        public int CreatureID { get; private set; }

        /// <summary>
        /// Name value.
        /// </summary>
        private string _name;
        public string Name { get => _name;  
            set { 
                if(value == null || value.Length < 1)
                {
                    throw new FormatException("Empty value for Name is not allowed.");
                }

                _name = value;
            } 
        }

        /// <summary>
        /// Age value.
        /// </summary>
        private int _age;
        public int Age { get => _age;  
            set { 
                if(value < 0)
                {
                    throw new IndexOutOfRangeException($"Age smaller 0 is not possible. Your input was '{value}'.");
                }

                _age = value;
            } 
        }

        /// <summary>
        /// Cargo -object for Item instances.
        /// </summary>
        public Inventory Items { get; private set; }

        /* Constructor */
        public Creature(string name, int age, CreatureType type)
        {
            Name = name;
            Age = age;
            CreatureType = type;

            Items = new Inventory(_defaultMaxCount);

            _creatureCount++;
            CreatureID = _creatureCount;
        }

        /// <summary>
        /// Adds a item to inventory list.
        /// </summary>
        /// <param name="item">Item which should be added.</param>
        /// <returns>True if adding was possible, otherwise false.</returns>
        public bool AddItem(Item item) {
            return Items.Add(item);
        }

        /// <summary>
        /// Removes item from Inventory.
        /// </summary>
        /// <param name="item">Item which should be removed.</param>
        /// <returns>The item which was removed if it was found, otherwise null.</returns>
        public Item RemoveItem(Item item)
        {
            return Items.Remove(i => i.ItemID == item.ItemID);
        }

        /// <summary>
        /// Gets the power of all weapons in the inventory.
        /// </summary>
        /// <returns>The calculated power of all weapons in the inventory.</returns>
        public int GetWholePower()
        {
            var power = 0;
            Items.GetWeapons().ForEach(w => {
                power += w.MagicPower;
            });

            return power;
        }
    }
}
