using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
using VillageManagement.Library;
using VillageManagement.Library.BaseItems;
using VillageManagement.Library.Creatures;
using VillageManagement.Library.Items;
using VillageManagement.Library.Tribes;

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Tribe<Creature> _selectedTribe;

        private Kingdom _kingdom;

        private const float _taxesForOneItem = 2.125f;

        private Calculator _calculator;

        public int WholePowerOfAll;

        /* Constructor */
        public MainPage()
        {
            InitializeComponent();

            if(ComponentService.Kingdom == null)
            {
                _kingdom = new Kingdom();
                _kingdom.LoadPreconditions();
            }
            else
            {
                _kingdom = ComponentService.Kingdom;
            }

            _kingdom.Tribes.ForEach(tribe => {
                TribesCb.Items.Add(tribe);
            });

            TribesCb.SelectedIndex = 0;
            if(ComponentService.SelectedTribe != null)
            {
                TribesCb.SelectedItem = ComponentService.SelectedTribe;
            }

            _selectedTribe = TribesCb.SelectedItem as Tribe<Creature>;

            TotalTaxesLabel.Content = GetTotalTaxes();

            UpdateTypeLabel();
        }

        /// <summary>
        /// Calculate and gets the total taxes value for all tribes.
        /// </summary>
        /// <returns>All taxes added up.</returns>
        private float GetTotalTaxes()
        {
            var itemCount = 0;
            _kingdom.Tribes.ForEach(tribe =>
            {
                tribe.Tribesmen.ForEach(tm => {
                    itemCount += tm.Items.GetWeapons().Count;
                }); 
            });

            return itemCount * _taxesForOneItem;
        }

        /// <summary>
        /// Change size of specific list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TribeMemberListSetup(object sender, SizeChangedEventArgs e)
        {
            var list = sender as ListView;

            var a = list.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            var col1 = 0.55;
            var col2 = 0.45;
            var col3 = 0.45;

            //TribeMemberGV.Columns[0].Width = a * col1;
            //TribeMemberGV.Columns[1].Width = a * col2;
            //if(TribeMemberGV.Columns.Count > 2)
            //{
            //    TribeMemberGV.Columns[2].Width = col3;
            //}
        }

        /// <summary>
        /// Prepares selected Tribe datas for showing in the list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TribesCbChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTribe = TribesCb.SelectedItem as Tribe<Creature>;

            var currentSelectedTribe = _kingdom.Tribes.FirstOrDefault(t => t == _selectedTribe);

            TribeMemberLV.ItemsSource = currentSelectedTribe.Tribesmen;
            TribeMemberLV.ContextMenu = CreateContextMenu();

            _calculator = new Calculator(_taxesForOneItem);
            TaxesTxtBox.Content = _calculator.CalculateTaxesOfTribe(currentSelectedTribe.Tribesmen).ToString();

            UpdateTypeLabel();
        }

        private ContextMenu CreateContextMenu()
        {
            var contextMenu = new ContextMenu();

            var menuItemEdit = new MenuItem { Header = "Edit" };
            var menuItemWeaponManager = new MenuItem { Header = "Item Manager" };

            menuItemEdit.Click += new RoutedEventHandler(ItemClick_CharacterEditor);
            menuItemWeaponManager.Click += new RoutedEventHandler(ItemClick_ItemManager);

            contextMenu.Items.Add(menuItemEdit);
            contextMenu.Items.Add(menuItemWeaponManager);
            return contextMenu;
        }

        /// <summary>
        /// Click -function to change to page of character editoring.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick_CharacterEditor(Object sender, RoutedEventArgs e)
        {
            ComponentService.SelectedCreature = TribeMemberLV.SelectedItem as Creature;
            ComponentService.Kingdom = _kingdom;

            var charEditPage = new CharEditPage();
            SendSelectedTribe();
            ComponentService.GetFrame().Content = charEditPage;
        }

        private void ItemClick_ItemManager(object sender, RoutedEventArgs e)
        {
            ComponentService.SelectedCreature = TribeMemberLV.SelectedItem as Creature;
            ComponentService.Kingdom = _kingdom;

            var itemManagementPage = new ItemManagement();
            SendSelectedTribe();
            ComponentService.GetFrame().Content = itemManagementPage;
        }

        private void SelectedChanged_ShowItems(object sender, SelectionChangedEventArgs e)
        {
            var selectedCreature = (sender as ListView).SelectedItem as Creature;
            if(selectedCreature == null)
            {
                ItemListView.ItemsSource = null;
                return;
            }

            ItemListView.ItemsSource = selectedCreature.Items.GetWeapons();
        }

        private void ButtonClick_OpenAddCreaturePage(object sender, RoutedEventArgs e)
        {
            ComponentService.Kingdom = _kingdom;
            SendSelectedTribe();
            ComponentService.GetFrame().Content = new AddCreaturePage();
        }

        private void ButtonClick_OpenWeaponManagerPage(object sender, RoutedEventArgs e)
        {
            ComponentService.Kingdom = _kingdom;
            SendSelectedTribe();
            ComponentService.GetFrame().Content = new WeaponManagerPage();
        }

        private void ButtonClick_OpenTribeCreator(object sender, RoutedEventArgs e)
        {
            ComponentService.Kingdom = _kingdom;
            SendSelectedTribe();
            ComponentService.GetFrame().Content = new AddTribe();
        }

        private void UpdateTypeLabel()
        {
            var types = _selectedTribe.AcceptableIntakes;
            string txt = "";
            var sectionCounter = 0;
            var maxCount = 4;
            for(var i = 0; i < types.Count; i++)
            {
                if(sectionCounter >= maxCount) { txt += "\n"; sectionCounter = 0; }
                sectionCounter++;
                if(i < 1)
                {
                    txt += types[i];
                    continue;
                }

                txt += " ," + types[i];
            }

            CreatureTypeLabel.Content = txt;
        }

        private void SendSelectedTribe()
        {
            ComponentService.SelectedTribe = TribesCb.SelectedItem as Tribe<Creature>;
        }
    }
}
