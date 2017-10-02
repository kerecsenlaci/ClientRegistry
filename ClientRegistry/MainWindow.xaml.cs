using CRegistry.Dal;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientRegistry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginVM loginVM = new LoginVM();
        public MainWindow()
        {
            InitializeComponent();
            if (loginVM.AuthenticateUser == null)
            {
                LoginWindow login = new LoginWindow() { DataContext = loginVM };
                login.ShowDialog();
                if (login.DialogResult != true)
                    Close();
            }
            DataContext = loginVM;
        }

        private void ShutdownPrograming(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            ShutdownPrograming(sender, e);
        }

        private void PartnerTypeClick(object sender, RoutedEventArgs e)
        {
            PartnerTypeWindow typeWindow = new PartnerTypeWindow();
            typeWindow.ShowDialog();
        }

        private void PartnersClick(object sender, RoutedEventArgs e)
        {
            string content;
            if(sender is Button)
            {
                Button button = (Button)sender;
                content = button.Content.ToString();
            }
            else
            {
                MenuItem menu = (MenuItem)sender;
                content = menu.Header.ToString();
            }

            if (content == "Partnerek")
            {
                PartnersVM partnersVM = new PartnersVM();
                PartnersWindow partners = new PartnersWindow { DataContext = partnersVM, Title=content};
                partners.Show();
            }
            else
            {
                ContactsVM contacts = new ContactsVM();
                PartnersWindow partners = new PartnersWindow { DataContext = contacts, Title=content };
                contacts.LoadPartnersList();
                partners.Show();
            }
        }

        private void PartnerQueryClick(object sender, RoutedEventArgs e)
        {
            QueryPartnerVM queryPartner = new QueryPartnerVM(((MenuItem)sender).Header.ToString());
            QueryWindow query = new QueryWindow() { DataContext = queryPartner };
            query.Show();
        }

        private void ContactQueryClick(object sender, RoutedEventArgs e)
        {
            QueryContactVM queryContact = new QueryContactVM(((MenuItem)sender).Header.ToString());
            QueryWindow query = new QueryWindow() { DataContext = queryContact };
            query.Show();
        }

        private void UserWindowClick(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow();
            window.ShowDialog();
        }
    }
}
