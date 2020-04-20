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
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Tribes;
using VillageManagement.Validation;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for AddCreaturePage.xaml
    /// </summary>
    public partial class AddCreaturePage : Page
    {
        private bool _isAgeValid;
        private bool _isNameValid;
        
        private int _ageTxtMaxLength = 10;
        private int _nameTxtMaxLength = 25;

        private string _name;
        private int _age;

        private Tribe<Creature> SelectedTribe;

        private ValidationHandler _validationHandler;

        private List<Tribe<Creature>> Tribes;

        private List<CreatureType> _types;

        /* Constructor */
        public AddCreaturePage()
        {
            Tribes = ComponentService.Kingdom.Tribes;

            InitializeComponent();

            _validationHandler = new ValidationHandler(Brushes.Red);

            _types = Enum.GetValues(typeof(CreatureType)).Cast<CreatureType>().ToList();

            TypeListBox.ItemsSource = _types;
            TypeListBox.SelectedIndex = 0;
            
            TribesListBox.ItemsSource = Tribes;
            if(TribesListBox.Items.Count > 0)
            {
                TribesListBox.SelectedIndex = 0;
                SelectedTribe = TribesListBox.SelectedItem as Tribe<Creature>;
            }
        }

        private void ButtonClick_Return(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }

        private void Keyup_CheckAge(object sender, KeyEventArgs e)
        {
            var newAgeTxtBox = sender as TextBox;


            var validationMsg = _validationHandler.CheckTextBoxCompleteForNumbers(newAgeTxtBox, _ageTxtMaxLength);
            if (!validationMsg.IsValid)
            {
                _isAgeValid = false;
                _validationHandler.SetBadNewsLabel(EmailValidationLabel, validationMsg.Message);

                return;
            }
            
            _isAgeValid = true;

            _validationHandler.SetDefaultLabel(EmailValidationLabel);

            int.TryParse(newAgeTxtBox.Text, out _age);

            CheckSaveBtn();
        }

        private void Keyup_CheckName(object sender, KeyEventArgs e)
        {
            var nameTxtBox = sender as TextBox;

            var validationMsg = _validationHandler.CheckTextBoxInputForRange(nameTxtBox, _nameTxtMaxLength);
            if (!validationMsg.IsValid)
            {
                _isNameValid = false;
                _validationHandler.SetBadNewsLabel(NameValidationLabel, validationMsg.Message);

                return;
            }

            _isNameValid = true;

            _validationHandler.SetDefaultLabel(NameValidationLabel);

            _name = nameTxtBox.Text;

            CheckSaveBtn();
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            var type = (CreatureType)TypeListBox.SelectedItem;
            if (type == CreatureType.Zwerg)
            {
                var creature = new Dwarf(_name, _age);
                SelectedTribe.AddTribesman(creature);

                CreatureStoryLabel.Content = $"Ein neuer {type} namens '{creature.Name}' betritt in seinem {creature.Age}. Jahr das Reich \n " +
                    $"und schließt sich dem Stam '{SelectedTribe.Name}' an.";

                return;
            }

            var nCr = new Dwarf(_name, _age);
            SelectedTribe.AddTribesman(nCr);

            CreatureStoryLabel.Content = $"Ein neuer {type.ToString()} namens '{nCr.Name}' betritt in seinem {nCr.Age}. Jahr das Reich \n " +
                $"und schließt sich dem Stam '{SelectedTribe.Name}' an.";
        }

        private void SelectionChangedListBox_ClickTribe(object sender, SelectionChangedEventArgs e)
        {
            SelectedTribe = (sender as ListBox).SelectedItem as Tribe<Creature>;

            CheckSaveBtn();
        }

        private void SelectionChanged_TypeListBox(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = (CreatureType)(sender as ListBox).SelectedItem;

            var a = Tribes.Where(tr => tr.AcceptableIntakes.Contains(selectedType));

            TribesListBox.ItemsSource = a;
            if(TribesListBox.Items.Count > 0)
            {
                TribesListBox.SelectedIndex = 0;
                SelectedTribe = TribesListBox.SelectedItem as Tribe<Creature>;
                return;
            }

            SelectedTribe = null;

            CheckSaveBtn();
        }

        private void CheckSaveBtn()
        {
            if (!_isAgeValid || !_isNameValid || SelectedTribe == null)
            {
                SaveBtn.IsEnabled = false;
                return;
            }

            SaveBtn.IsEnabled = true;
        }
    }
}
