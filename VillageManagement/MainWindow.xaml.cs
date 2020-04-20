using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object SelectedComboItem { get; set; }

        private string _selectedTribeName;
        
        public int WholePowerOfAll;

        private Kingdom _kingdom;

        public float Taxes { get; private set; }

        private float _taxesForOneItem = 2125;

        private Calculator _calculator;

        /* Constructor */
        public MainWindow()
        {
            _calculator = new Calculator(_taxesForOneItem);

            InitializeComponent();

            ComponentService.RequireFrame(MFrame);

            //var mainPage = new MainPage();
            var mainPage = new MainPage();
            MFrame.Content = mainPage;
        }
    }
}
