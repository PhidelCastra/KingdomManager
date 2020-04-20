using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.BaseItems
{
    public abstract class Item
    {
        /// <summary>
        /// Maximum count of different item types.
        /// </summary>
        private const int _maxTypeNumber = 4;

        /// <summary>
        /// Count item with each instantiated object of this class.
        /// </summary>
        private static int _itemCount;

        public bool InInventory { get; set; }

        /// <summary>
        /// ID value for object. Should be declareted with an object counter.
        /// </summary>
        public int ItemID { get; private set; }

        /// <summary>
        /// Type number (should be dependent by max number of types).
        /// </summary>
        private int _itemType;
        public int ItemType { get => _itemType; internal set { 
                if(value < 0 || value > _maxTypeNumber)
                {
                    throw new IndexOutOfRangeException($"Input '{value}' was not in range of 0 and {_maxTypeNumber}.");
                }

                _itemType = value;
            } 
        }

        /// <summary>
        /// Name value.
        /// </summary>
        private string _itemName;
        public string ItemName { get => _itemName; set { 
                if(value.Length < 1)
                {
                    throw new ArgumentNullException("Item name value -length is 0 - It´s not allowed.");
                }

                _itemName = value;
            } 
        }

        /* Constructor */
        public Item(string name)
        {
            ItemType = 1;
            ItemName = name;

            _itemCount++;
            ItemID = _itemCount;
        }
    }
}
