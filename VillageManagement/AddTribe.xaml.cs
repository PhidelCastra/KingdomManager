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
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Tribes;
using VillageManagement.Validation;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for AddTribe.xaml
    /// </summary>
    public partial class AddTribe : Page
    {
        /// <summary>
        /// CreatureType -List for AllType -ListBox.
        /// </summary>
        private ObservableCollection<CreatureType> _allTypes;

        /// <summary>
        /// CreatureType -List for AcceptableIntakes -property of the Tribe -class.
        /// </summary>
        private ObservableCollection<CreatureType> _ownTypes;

        /// <summary>
        /// ValidationHandler which includes some methods to check validation.
        /// </summary>
        private ValidationHandler _validationHandler;

        private bool _isFoundationYearValid;
        private bool _isNameValid;

        /// <summary>
        /// Maximum acceptable foundation year.
        /// </summary>
        private int _foundationYearTxtMaxLength = 10000;
        private int _nameMaxLength = 40;

        /// <summary>
        /// FoundationYear -value for the new created Tribe.
        /// </summary>
        private int _foundationYear = -1;

        /// <summary>
        /// Name -value for the new created Tribe.
        /// </summary>
        private string _name;

        /* Constructor */
        public AddTribe()
        {
            _validationHandler = new ValidationHandler(Brushes.DarkRed);

            _ownTypes = new ObservableCollection<CreatureType>();
            _allTypes = new ObservableCollection<CreatureType>(Enum.GetValues(typeof(CreatureType)).Cast<CreatureType>()); 

            InitializeComponent();

            AllTypesListBox.ItemsSource = _allTypes;
            OwnTypesListBox.ItemsSource = _ownTypes;
        }

        private void ClickBtn_SwapItem(object sender, RoutedEventArgs e)
        {
            if (AllTypesListBox.SelectedItem != null)
            {
                var selected = (CreatureType)AllTypesListBox.SelectedItem;
                _allTypes.Remove(selected);
                _ownTypes.Add(selected);
            }

            if (OwnTypesListBox.SelectedItem != null)
            {
                var selected = (CreatureType)OwnTypesListBox.SelectedItem;
                _ownTypes.Remove(selected);
                _allTypes.Add(selected);
            }

            TribeStoryLabel.Content = string.Empty;
            PrepareSwapWeaponButton(false);
            CheckSaveBtn();
        }

        private void Keyup_CheckAge(object sender, KeyEventArgs e)
        {
            var newAgeTxtBox = sender as TextBox;


            var validationMsg = _validationHandler.CheckTextBoxCompleteForNumbers(newAgeTxtBox, _foundationYearTxtMaxLength);
            if (!validationMsg.IsValid)
            {
                _isFoundationYearValid = false;
                _validationHandler.SetBadNewsLabel(FoundationValidationLabel, validationMsg.Message);
            }
            else
            {
                _isFoundationYearValid = true;

                _validationHandler.SetDefaultLabel(FoundationValidationLabel);

                int.TryParse(newAgeTxtBox.Text, out _foundationYear);
            }

            CheckSaveBtn();
        }

        private void SelectionChanged_ListBox(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == AllTypesListBox)
            {
                OwnTypesListBox.SelectedIndex = -1;
            }

            if (listBox == OwnTypesListBox)
            {
                AllTypesListBox.SelectedIndex = -1;
            }

            PrepareSwapWeaponButton(true);
        }

        /// <summary>
        /// Checks the validation of "sender" -input.
        /// </summary>
        /// <param name="sender">Should be a RichTextBox.</param>
        /// <param name="e"></param>
        private void Keyup_CheckName(object sender, KeyEventArgs e)
        {
            var nameTxtBox = sender as TextBox;

            var validationMsg = _validationHandler.CheckTextBoxInputForRange(nameTxtBox, _nameMaxLength);
            if (!validationMsg.IsValid)
            {
                _isNameValid = false;
                _validationHandler.SetBadNewsLabel(NameValidationLabel, validationMsg.Message);
            }
            else
            {
                _isNameValid = true;

                _validationHandler.SetDefaultLabel(NameValidationLabel);

                _name = nameTxtBox.Text;
            }
    
            CheckSaveBtn();
        }

        private void CheckSaveBtn()
        {
            if (!_isFoundationYearValid || !_isNameValid || _ownTypes.Count < 1)
            {
                SaveBtn.IsEnabled = false;
                return;
            }

            SaveBtn.IsEnabled = true;
        }

        private void PrepareSwapWeaponButton(bool state)
        {
            SwapTypeButton.IsEnabled = state;
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            var typeList = _ownTypes.ToList();
            var tribe = new Tribe<Creature>(_name, _foundationYear, _ownTypes[0]);
            typeList.ForEach(type => {
                tribe.AddAcceptableIntake(type);
            });

            ComponentService.Kingdom.Tribes.Add(tribe);

            var story = $"Der Stamm '{_name}', welcher seit dem Jahre '{_foundationYear}' existiert, wurde entdeckt.\n " +
                $"Der Stamm besteht aus Angehörigen der Rasse {typeList[0]} \n";
            if(typeList.Count > 1)
            {
                story += "ebenso sind Mitglieder der Spezies ";
                for(var i = 1; i < typeList.Count; i++)
                {
                    if(i + 1 >= typeList.Count && typeList.Count > 2)
                    {
                        story += $"und {typeList[i]} ";
                        continue;
                    }

                    story += $"{typeList[i]}";
                    if(i + 2 < typeList.Count && typeList.Count > 2) { 
                        story += ", ";
                    }
                    else
                    {
                        story += " ";
                    }

                }

                story += "vertreten.";
            }

            TribeStoryLabel.Content = story;
        }

        private void BtnClick_ReturnMain(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }
    }
}
