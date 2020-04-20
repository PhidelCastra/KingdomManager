using System;
using System.Collections.Generic;
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
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Items;
using VillageManagement.Validation;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for CharEditPage.xaml
    /// </summary>
    public partial class CharEditPage : Page
    {
        public string ValidationMessage;

        private Brush _defaultTextColor;

        private int _ageTxtMaxLength = 10;
        private int _nameTxtMaxLength = 25;

        private bool _isAgeValid = false;
        private bool _isNameValid = false;

        private string _newName;
        private int? _newAge;

        private string _ageNotANumberMsg = "Age should be a number!";
        private string _maxNumberIs = "Maximum number of characters is '{0}'";

        private ValidationHandler _validationHandler;

        public CharEditPage()
        {
            _validationHandler = new ValidationHandler(Brushes.Red);

            InitializeComponent();

            // Create creature infos 
            CurrentNameLabel.Content = ComponentService.SelectedCreature.Name;
            CurrentAgeLabel.Content = ComponentService.SelectedCreature.Age;

            CurrentNameHeaderLabel.Content = ComponentService.SelectedCreature.Name;

            AllWeaponsListBox.ItemsSource = ComponentService.AvailableWeapons;
            AllWeaponsListBox.SelectedIndex = -1;

            OwnWeaponsListBox.ItemsSource = ComponentService.SelectedCreature.Items.GetWeapons();
            OwnWeaponsListBox.SelectedIndex = -1;

            _defaultTextColor = Brushes.Black;
        }

        private void BtnClick_ReturnMain(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            ComponentService.SelectedCreature.Name = _newName != null ? _newName : ComponentService.SelectedCreature.Name;
            ComponentService.SelectedCreature.Age = _newAge != null ? (int)_newAge : ComponentService.SelectedCreature.Age;

            foreach(var tribe in ComponentService.Kingdom.Tribes)
            {
                var cr = tribe.Tribesmen.FirstOrDefault(tm => tm.CreatureID == ComponentService.SelectedCreature.CreatureID);
                if (cr != null)
                {
                    cr = ComponentService.SelectedCreature;
                    break;
                }
            }

            CurrentNameLabel.Content = ComponentService.SelectedCreature.Name;
            CurrentAgeLabel.Content = ComponentService.SelectedCreature.Age;
        }

        /// <summary>
        /// Checks for age -value in the corresponding TextBox.
        /// </summary>
        /// <param name="sender">Should be a TextBox -component.</param>
        /// <param name="e">Type of the event.</param>
        private void Keyup_CheckAge(object sender, KeyEventArgs e)
        {
            var newAgeTxtBox = sender as TextBox;
            
            _isAgeValid = false;
            CheckSaveBtn();

            if (newAgeTxtBox.Text.Length >= _ageTxtMaxLength)
            {
                newAgeTxtBox.Foreground = Brushes.Red;

                var msg = string.Format(_maxNumberIs, _ageTxtMaxLength);

                _validationHandler.SetBadNewsLabel(EmailValidationLabel, msg);
                return;
            }

            var validMsg = _validationHandler.CheckTextBoxForNumbers(newAgeTxtBox);
            if (newAgeTxtBox.Text != string.Empty && !validMsg.IsValid)
            {
                newAgeTxtBox.Foreground = Brushes.Red;

                _validationHandler.SetBadNewsLabel(EmailValidationLabel, _ageNotANumberMsg);
                return;
            }

            newAgeTxtBox.Foreground = _defaultTextColor;

            _validationHandler.SetDefaultLabel(EmailValidationLabel);

            _isAgeValid = true;
            _newAge = ConvertStringToInt(newAgeTxtBox.Text);

            CheckSaveBtn();
        }

        /// <summary>
        /// Checks for name -value in the corresponding TextBox.
        /// </summary>
        /// <param name="sender">Should be a TextBox -component.</param>
        /// <param name="e">Type of the event.</param>
        private void Keyup_CheckName(object sender, KeyEventArgs e)
        {
            var nameTxtBox = sender as TextBox;

            _isNameValid = false;
            CheckSaveBtn();

            _newName = null;

            if(nameTxtBox.Text != string.Empty && nameTxtBox.Text[0] == ' ')
            {
                nameTxtBox.Text = string.Empty;
                return;
            }

            if(nameTxtBox.Text.Length >= _nameTxtMaxLength)
            {
                _validationHandler.SetBadNewsLabel(NameValidationLabel, $"Maximum number of characters is '{_nameTxtMaxLength}'.");
                return;
            }

            _isNameValid = true;
            _newName = NameTextBox.Text;
            _validationHandler.SetDefaultLabel(NameValidationLabel);

            CheckSaveBtn();
        }

        /// <summary>
        /// Checks the validation -booleans which are dependent from the corresponding inputs.
        /// </summary>
        private void CheckSaveBtn()
        {
            if(_isAgeValid && AgeTextBox.Text != string.Empty && NameTextBox.Text == string.Empty ||
                _isNameValid && NameTextBox.Text != string.Empty && AgeTextBox.Text == string.Empty || 
                _isNameValid && _isAgeValid && AgeTextBox.Text != string.Empty || NameTextBox.Text != string.Empty)
            {
                SaveBtn.IsEnabled = true;
                return;
            }

            SaveBtn.IsEnabled = false;
        }

        private int? ConvertStringToInt(string txt)
        {
            if (!int.TryParse(txt, out int nr))
            {
                return null;
            }

            return nr;
        }

        private void ClickBtn_SwapItem(object sender, RoutedEventArgs e)
        {
            if(AllWeaponsListBox.SelectedItem != null)
            {
                var selected = AllWeaponsListBox.SelectedItem as Weapon;
                ComponentService.SelectedCreature.AddItem(selected);
                ComponentService.AvailableWeapons.Remove(selected);

                OwnWeaponsListBox.ItemsSource = ComponentService.SelectedCreature.Items.GetWeapons();
            }

            if(OwnWeaponsListBox.SelectedItem != null)
            {
                var selected = OwnWeaponsListBox.SelectedItem as Weapon;
                ComponentService.SelectedCreature.RemoveItem(selected);
                ComponentService.AvailableWeapons.Add(selected);

                OwnWeaponsListBox.ItemsSource = ComponentService.SelectedCreature.Items.GetWeapons();
            }
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
        }
    }
}
