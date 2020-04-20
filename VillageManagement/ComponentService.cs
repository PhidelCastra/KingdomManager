using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using VillageManagement.Library;
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Items;
using VillageManagement.Library.Tribes;

namespace VillageManagement
{
    static class ComponentService
    {
        private static Frame _frame;

        public static Creature SelectedCreature;

        public static Kingdom Kingdom;

        public static ObservableCollection<Weapon> AvailableWeapons = new ObservableCollection<Weapon>();

        public static void RequireFrame(Frame frame)
        {
            _frame = frame;
        }

        public static Frame GetFrame()
        {
            return _frame;
        }
    }
}
