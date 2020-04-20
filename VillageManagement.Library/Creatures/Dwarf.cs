using System;
using System.Collections.Generic;

namespace VillageManagement.Library.Creatures
{
    public class Dwarf : Creature
    {
        /* Constructor */ 
        public Dwarf(string name, int age) 
            : base(name, age, CreatureType.Zwerg)
        {
        }

        // TODO: This should be a part of creature(?)...
        /// <summary>
        /// Sets maximum items in the inventory.
        /// </summary>
        /// <param name="count"></param>
        public void SetMaxItemCount(int count)
        {
            if(Items.Count() > count)
            {
                throw new FieldAccessException($"Not possible to change list if it´s item count is higher ('{Items.Count()}') then the new count of '{count}'.");
            }

            Items.MaxCount = count;   
        }
    }
}
