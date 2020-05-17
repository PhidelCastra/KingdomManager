using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VillageManagement.ExceptionHandling.Exceptions;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Jobs;

namespace VillageManagement.Library.Tribes
{
    public class Tribe<T> where T : Creature
    {
        /// <summary>
        /// Increments one for each Tribe and pass it as ID -value.
        /// </summary>
        private static int _idCounter;

        /// <summary>
        /// ID should be different for each created Tribe instance. 
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Name value of Trieb.
        /// </summary>
        public string Name { get; set; }

        public List<CreatureType> AcceptableIntakes { get; set; }

        /// <summary>
        /// List with all citiziens of this Tribe.
        /// </summary>
        public List<Creature> Tribesmen { get; private set; }

        /// <summary>
        /// Chief -Creature of this Tribe.
        /// </summary>
        private T _chief;
        public T Chief { get => _chief; set { 
                if(Tribesmen.FirstOrDefault(tm => tm.CreatureID == value.CreatureID) == null)
                {
                    throw new MissingMemberException($"'{value.Name}' is not member of this Tribe - Adding to chief is not possible.");
                }

                if(_chief != null) {
                    _chief.IsChief = false;
                }

                _chief = value;
                _chief.IsChief = true;
            } 
        }

        /// <summary>
        /// Foundation year -value.
        /// </summary>
        private int _foundationYear;
        public int FoundationYear { get => _foundationYear; 
            private set 
            { 
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Value '{value}' for year is out of range - Value must be higher then 0.");
                }

                _foundationYear = value;
            }
        }

        /* Constructor */
        public Tribe(string name, int foundationYear, CreatureType foundationType)
        {
            ID = _idCounter;
            _idCounter++;

            Name = name;
            FoundationYear = foundationYear;

            AcceptableIntakes = new List<CreatureType>();
            AcceptableIntakes.Add(foundationType);

            Tribesmen = new List<Creature>();
        }

        /// <summary>
        /// Adds a Creature to tribes list.
        /// </summary>
        /// <param name="creature">Creature which should be added.</param>
        public void AddTribesman(T creature)
        {
            if(Tribesmen.FirstOrDefault(c => c.CreatureID == creature.CreatureID) != null)
            {
                throw new MulticastNotSupportedException($"Tribe '{Name}' includes already Creature: '{creature.Name}' with ID: '{creature.CreatureID}'.");
            }

            if(!AcceptableIntakes.Exists(type => type == creature.CreatureType))
            {
                throw new CreatureTypeIsNotAllowedException(creature.CreatureType.ToString(), Name);
            }

            Tribesmen.Add(creature);
        }

        /// <summary>
        /// Removes a Creature from tribes list.
        /// </summary>
        /// <param name="creature">Creature which should be removed.</param>
        /// <returns>True if the Creature was found, otherwise false.</returns>
        public bool RemoveTribesman(T creature)
        {
            return Tribesmen.Remove(creature);
        }

        /// <summary>
        /// Adds a Creature type to make possible adding other creatures to this tribe.
        /// </summary>
        /// <param name="creatureType">Enum creature type.</param>
        /// <returns>True if the creature type was not includes already, otherwise false.</returns>
        public bool AddAcceptableIntake(CreatureType creatureType)
        {
            if (AcceptableIntakes.Contains(creatureType))
            {
                return false;
            }

            AcceptableIntakes.Add(creatureType);

            return true;
        }

        /// <summary>
        /// Remove a creature type from this tribe and delete also all corresponding Creatures from this tribe.
        /// </summary>
        /// <param name="type">Creature type which should be deleted.</param>
        /// <returns>List with all removed Creatures.</returns>
        public List<Creature> RemoveAcceptableIntake(CreatureType type)
        {
            if (!AcceptableIntakes.Contains(type))
            {
                return null;
            }

            var removedCreatures = Tribesmen.Where(tm => tm.CreatureType == type);
            Tribesmen = Tribesmen.Where(tm => tm.CreatureType != type).ToList();

            return removedCreatures.ToList();
        }

        /// <summary>
        /// Calculate all weaponspower -values, of the tribsemens, together and gets the value.
        /// </summary>
        /// <returns>Compounded power of all weapons.</returns>
        public int GetWholePower()
        {
            var wholePower = 0;
            Tribesmen.ForEach(tm => {
                wholePower += tm.GetWholePower();
            });

            return wholePower;
        }
    }
}
