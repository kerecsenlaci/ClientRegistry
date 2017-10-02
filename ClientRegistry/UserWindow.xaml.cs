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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        UserWindowVM userVM = new UserWindowVM();
        public UserWindow()
        {
            InitializeComponent();
            DataContext = userVM;
        }

        private void NewPartnerTypeClick(object sender, RoutedEventArgs e)
        {
            userVM.SelectedUser = new User();
            userVM.IsEnabled = true;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (userVM.SelectedUser != null)
                if (!userVM.SaveUser(PassBox1.Password, PassBox2.Password))
                    MessageBox.Show("Hibás adatbevitel", "Figyelmeztetés");
            PassBox1.Password = "";
            PassBox2.Password = "";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userVM.IsEnabled = false;
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            if (userVM.SelectedUser != null)
                userVM.IsEnabled = true;
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (userVM.SelectedUser != null)
                if (MessageBoxResult.Yes == MessageBox.Show("Biztos hogy törlöni szeretné?", "Figyelmeztetés", MessageBoxButton.YesNo) && !userVM.RemoveUser())
                    MessageBox.Show("Nem választott elemet!\n\rVagy nem mentett elemet szeretne törölni!", "Figyelmeztetés");
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (userVM.IsEnabled && userVM.ValidateUser())
            {
                var result = MessageBox.Show("Szerkesztés alatt van!\n\rSzeretné menteni a változásokat?", "Figyelmeztetés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    if (!userVM.SaveUser(PassBox1.Password,PassBox2.Password))
                    {
                        MessageBox.Show("Hibás adatbevitel", "Figyelmeztetés");
                        e.Cancel = true;
                    }

            }
        }
    }
}
