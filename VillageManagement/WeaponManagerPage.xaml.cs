using System;
using System.Collections.Generic;
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
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Items;
using VillageManagement.Validation;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for WeaponManagerPage.xaml
    /// </summary>
    public partial class WeaponManagerPage : Page
    {
        private ValidationHandler _validationHandler;

        private int _maxWeaponPower = 10;

        private ItemHandler _itemHandler;

        private List<string> _typeList = new List<string> { "Axt", "Sword", "Magic Stick", "QarrelingHammer", "Bow", "Knife", "Spear" };

        public WeaponManagerPage()
        {
            InitializeComponent();

            _validationHandler = new ValidationHandler(Brushes.Red);

            _itemHandler = new ItemHandler();

            RefreshWeaponListView();

            CategoryComboBox.ItemsSource = _typeList;
            CategoryComboBox.SelectedIndex = 0;
        }

        private void BtnClick_ReturnMain(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }

        private void Keyup_PowerTextBox(object sender, KeyEventArgs e)
        {
           var powerTxtBox = sender as TextBox;

            var validationMsg = _validationHandler.CheckTextBoxCompleteForNumbers(powerTxtBox, _maxWeaponPower);
            if (!validationMsg.IsValid)
            {
                _validationHandler.SetBadNewsLabel(PowerValidationLabel, validationMsg.Message);
                SaveBtn.IsEnabled = false;

                return;
            }

            _validationHandler.SetDefaultLabel(PowerValidationLabel);
            SaveBtn.IsEnabled = true;
        }

        private void BtnClick_Save(object sender, RoutedEventArgs e)
        {
            var selectedType = CategoryComboBox.SelectedItem;

            int power;
            int.TryParse(PowerTextBox.Text, out power);

            Weapon item;
            switch (selectedType)
            {
                case "Axt":
                    item = _itemHandler.CreateAxt(power);
                    break;
                case "Magic Stick":
                    item = _itemHandler.CreateMagicStick(power);
                    break;
                case "Sword":
                    item = _itemHandler.CreateSword(power);
                    break;
                case "QarrelingHammer":
                    item = _itemHandler.CreateQarrelingHammer(power);
                    break;
                case "Bow":
                    item = _itemHandler.CreateBow(power);
                    break;
                case "Knife":
                    item = _itemHandler.CreatKnife(power);
                    break;
                case "Spear":
                    item = _itemHandler.CreateSpear(power);
                    break;
                default:
                    throw new Exception($"The weapon type '{selectedType}' is not available.");
            }

            ComponentService.AvailableWeapons.Add(item);

            RefreshWeaponListView();
        }

        private void RefreshWeaponListView()
        {
            WeaponListView.ItemsSource = ComponentService.AvailableWeapons;
        }
    }
}
