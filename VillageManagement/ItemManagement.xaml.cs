using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VillageManagement.Library.Items;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for ItemManagement.xaml
    /// </summary>
    public partial class ItemManagement : Page
    {
        private ObservableCollection<Weapon> _allItems;
        private ObservableCollection<Weapon> _ownItems;

        public ItemManagement()
        {
            _allItems = ComponentService.AvailableWeapons;
            _ownItems = new ObservableCollection<Weapon>(ComponentService.SelectedCreature.Items.GetWeapons());

            InitializeComponent();

            HeaderLabel.Content = ComponentService.SelectedCreature.Name + $" ({ComponentService.SelectedCreature.CreatureType})";

            AllWeaponsListBox.ItemsSource = _allItems;
            OwnWeaponsListBox.ItemsSource = _ownItems;
        }

        private void BtnClick_ReturnMain(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }

        private void ClickBtn_SwapItem(object sender, RoutedEventArgs e)
        {
            if (AllWeaponsListBox.SelectedItem != null)
            {
                var selected = AllWeaponsListBox.SelectedItem as Weapon;
                if (!ComponentService.SelectedCreature.AddItem(selected))
                {
                    var msg = $"The inventory of character '{ComponentService.SelectedCreature.Name}' is already full.";
                    MessageLabel.Content = msg;
                    return;
                }

                ComponentService.AvailableWeapons.Remove(selected);
                OwnWeaponsListBox.ItemsSource = ComponentService.SelectedCreature.Items.GetWeapons();
            }

            if (OwnWeaponsListBox.SelectedItem != null)
            {
                var selected = OwnWeaponsListBox.SelectedItem as Weapon;
                ComponentService.SelectedCreature.RemoveItem(selected);
                ComponentService.AvailableWeapons.Add(selected);

                OwnWeaponsListBox.ItemsSource = ComponentService.SelectedCreature.Items.GetWeapons();
            }

            MessageLabel.Content = string.Empty;
            PrepareSwapWeaponButton(false);
        }

        private void SelectionChanged_ListBox(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == AllWeaponsListBox)
            {
                OwnWeaponsListBox.SelectedIndex = -1;
            }

            if (listBox == OwnWeaponsListBox)
            {
                AllWeaponsListBox.SelectedIndex = -1;
            }

            PrepareSwapWeaponButton(true);
        }

        private void PrepareSwapWeaponButton(bool state)
        {
            SwapWeaponButton.IsEnabled = state;
        }
    }
}
