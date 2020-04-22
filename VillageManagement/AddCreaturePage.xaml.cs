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
using VillageManagement.ExceptionHandling.Exceptions;
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

            if(TypeListBox.SelectedItem != null)
            {
                var selectedType = (CreatureType)TypeListBox.SelectedItem;
                var allowedTribes = Tribes.Where(tribe => tribe.AcceptableIntakes.Contains(selectedType));
                TribesListBox.ItemsSource = allowedTribes;
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
                _validationHandler.SetBadNewsLabel(FoundationValidationLabel, validationMsg.Message);
            }
            else
            {
                _isAgeValid = true;

                _validationHandler.SetDefaultLabel(FoundationValidationLabel);

                int.TryParse(newAgeTxtBox.Text, out _age);
            }
            
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
            }
            else
            {
                _isNameValid = true;

                _validationHandler.SetDefaultLabel(NameValidationLabel);

                _name = nameTxtBox.Text;
            }

            CheckSaveBtn();
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            var type = (CreatureType)TypeListBox.SelectedItem;

            Creature creature;
            switch (type)
            {
                case CreatureType.Zwerg:
                    creature = new Dwarf(_name, _age);
                    break;
                case CreatureType.Elb:
                    creature = new Elf(_name, _age);
                    break;
                case CreatureType.Mensch:
                    creature = new Human(_name, _age);
                    break;
                case CreatureType.Hobbit:
                    creature = new Hobbit(_name, _age);
                    break;
                case CreatureType.Ork:
                    creature = new Ork(_name, _age);
                    break;
                case CreatureType.Gnom:
                    creature = new Gnome(_name, _age);
                    break;
                case CreatureType.Hochelf:
                    creature = new HighElf(_name, _age);
                    break;
                default:
                    throw new CreatureTypeNotAvailableException(type.ToString());
            }

            SelectedTribe.AddTribesman(creature);
            CreatureStoryLabel.Content = $"Ein neuer {type} namens '{creature.Name}' betritt in seinem {creature.Age}. Jahr das Reich \n " +
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
