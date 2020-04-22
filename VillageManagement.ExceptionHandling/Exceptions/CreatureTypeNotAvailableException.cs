using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.ExceptionHandling.Exceptions
{
    public class CreatureTypeNotAvailableException : Exception
    {
        public CreatureTypeNotAvailableException() 
            : base()
        {
        }

        public CreatureTypeNotAvailableException(string type)
            : base($"Creature type '{type}' was not available.")
        {
        }
    }
}
