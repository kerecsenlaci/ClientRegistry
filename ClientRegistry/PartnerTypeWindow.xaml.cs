using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientRegistry
{
    /// <summary>
    /// Interaction logic for PartnerTypeWindow.xaml
    /// </summary>
    public partial class PartnerTypeWindow : Window
    {
        public PartnerTypeWindow()
        {
            InitializeComponent();
        }

        private void NewPartnerTypeClick(object sender, RoutedEventArgs e)
        {
            var newPartnertype = ((PartnerTypeVM)DataContext).SelectedType;
            newPartnertype = new partnertype();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (((PartnerTypeVM)DataContext).SelectedType.ValidateType())
            {
                RegistryModel registry = new RegistryModel();
                registry.partnertype.Add(((PartnerTypeVM)DataContext).SelectedType);
                registry.SaveChanges();
            }
            else
                MessageBox.Show("Hibás adatbevitel", "Figyelmeztetés");

        }
    }
}
