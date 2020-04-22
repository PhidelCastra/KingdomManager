using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.ExceptionHandling.Exceptions
{
    public class CreatureTypeIsNotAllowedException : Exception
    {
        public CreatureTypeIsNotAllowedException() 
            : base()
        {
        }

        public CreatureTypeIsNotAllowedException(string type, string tribename)
            : base($"'{type}' is not allowed in this tribe '{tribename}'.")
        {
        }
    }
}
