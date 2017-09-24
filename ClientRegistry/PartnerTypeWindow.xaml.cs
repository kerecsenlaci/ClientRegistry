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
        PartnerTypeVM typeVM = new PartnerTypeVM();
        public PartnerTypeWindow()
        {
            InitializeComponent();
            DataContext = typeVM;
        }

        private void NewPartnerTypeClick(object sender, RoutedEventArgs e)
        {
            typeVM.SelectedType = new PartnerType();
            typeVM.IsEnabled = true;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (typeVM.SelectedType!=null)
                if (!typeVM.SavePartnerType())
                    MessageBox.Show("Hibás adatbevitel", "Figyelmeztetés");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeVM.IsEnabled = false;
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            if(typeVM.SelectedType!=null)
                typeVM.IsEnabled = true;
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if(typeVM.SelectedType!=null)
                if (MessageBoxResult.Yes == MessageBox.Show("Biztos hogy törlöni szeretné?","Figyelmeztetés",MessageBoxButton.YesNo) && !typeVM.RemovePartnerType())
                    MessageBox.Show("Nem választott elemet!\n\rVagy nem mentett elemet szeretne törölni!", "Figyelmeztetés");
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (typeVM.IsEnabled && typeVM.SelectedType.ValidateType())
            {
                var result = MessageBox.Show("Szerkesztés alatt van!\n\rSzeretné menteni a változásokat?", "Figyelmeztetés", MessageBoxButton.YesNo);
                if(result==MessageBoxResult.Yes)
                    if (!typeVM.SavePartnerType())
                    {
                        MessageBox.Show("Hibás adatbevitel", "Figyelmeztetés");
                        e.Cancel = true;
                    }
                        
            }
        }
    }
}
