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

namespace VillageManagement
{
    /// <summary>
    /// Interaction logic for TribeManagement.xaml
    /// </summary>
    public partial class TribeManagement : Page
    {
        public TribeManagement()
        {
            InitializeComponent();
        }

        private void BtnClick_ReturnMain(object sender, RoutedEventArgs e)
        {
            ComponentService.GetFrame().Content = new MainPage();
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            foreach (var tribe in ComponentService.Kingdom.Tribes)
            {
                var cr = tribe.Tribesmen.FirstOrDefault(tm => tm.CreatureID == ComponentService.SelectedCreature.CreatureID);
                if (cr != null)
                {
                    cr = ComponentService.SelectedCreature;
                    break;
                }
            }
        }
    }
}
