using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VillageManagement.Library.BaseItems;

namespace VillageManagement.Library.Items
{
    public class Inventory
    {
        /// <summary>
        /// Maximum count of allowed item in the list.
        /// </summary>
        private int _maxCount;
        public int MaxCount { get => _maxCount; set {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException($"Item count smaller '{value}' is not possible!");
                }

                if (_items != null && _items.Count() > value)
                {
                    throw new FieldAccessException($"Not possible to change list if it´s item count is higher ('{_items.Count()}') then the new count of '{value}'.");
                }

                _maxCount = value;
            } 
        }

        /// <summary>
        /// List with Item -objects.
        /// </summary>
        private List<Item> _items;
        
        /* Constructor */ 
        public Inventory(int maxCount)
        {
            MaxCount = maxCount;

            _items = new List<Item>();
        }

        /// <summary>
        /// Adds new Item if possible.
        /// </summary>
        /// <param name="item">Item which should be added.</param>
        /// <returns>True if the item was not in the List already or max count was reached, otherwise false.</returns>
        public bool Add(Item item)
        {
            if(_items.Count >= MaxCount)
            {
                return false;
            }

            if (item.InInventory)
            {
                throw new Exception($"Item '{item.ItemName}' is already in a inventory. Multiply adding is not possible.");
            }

            _items.Add(item);
            item.InInventory = true;

            return true;
        }

        /// <summary>
        /// Removes a specific Item from Item list.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Item Remove(Func<Item, bool> query)
        {
            var itemToRemove = _items.FirstOrDefault(query);
            if(itemToRemove == null)
            {
                return null;
            }

            _items.Remove(itemToRemove);
            itemToRemove.InInventory = false; 

            return itemToRemove;
        }

        /// <summary>
        /// Gets count of the Item list.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _items.Count;
        }

        /// <summary>
        /// Gets all weapons from inventory.
        /// </summary>
        /// <returns>List with all weapons.</returns>
        public List<Weapon> GetWeapons()
        {
            var weapons = _items.Cast<Weapon>().ToList();

            return weapons;
        }
    }
}
