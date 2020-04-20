using System;
using System.Collections.Generic;
using System.Text;
using VillageManagement.Library.Creatures;

namespace VillageManagement
{
    public class Calculator
    {
        private float _taxesPerItem;
        public float TaxesPerItem { get => _taxesPerItem; set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Value '{value}' can´t use to taxes. Value, lower then 0, is not allowed.");
                }

                _taxesPerItem = value;
            } 
        }

        public Calculator(float taxesPerItem)
        {
            TaxesPerItem = taxesPerItem;
        }

        /// <summary>
        /// Calculates and gets taxes for one Tribe.
        /// </summary>
        /// <param name="creatures">Tribe to calculate it´s taxes.</param>
        /// <returns>Taxes value, dependent from each item.</returns>
        public float CalculateTaxesOfTribe(List<Creature> creatures)
        {
            var taxes = 0;
            creatures.ForEach(c => {
                taxes += c.Items.Count();
            });

            return taxes * TaxesPerItem;
        }
    }
}
