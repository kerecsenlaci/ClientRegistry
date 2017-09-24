using System.Windows;

namespace ClientRegistry
{
    /// <summary>
    /// Interaction logic for PartnerFormWindow.xaml
    /// </summary>
    public partial class PartnerFormWindow : Window
    {
        public PartnerFormWindow()
        {
            InitializeComponent();
        }

        private void SavePartnerForm(object sender, RoutedEventArgs e)
        {
            string message;
            var formVM = DataContext as PartnerFormVM;
            if (!formVM.PartnerValidate(out message))
                MessageBox.Show(message);
            else
            {
                formVM.SavePartner();
                formVM.CopyChosenPartner();
                Close();
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            var formVM = DataContext as PartnerFormVM;
            if (formVM.IsEdit && formVM.IsModified())
            {
                var result = MessageBox.Show("Nem mentett változások vannak.\n\rSzeretné menteni?", "Figyelmeztetés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    formVM.SavePartner();
                else
                    formVM.RestoreContact();
            }
        }

        private void ContactAddClick(object sender, RoutedEventArgs e)
        {
            ContactsVM contacts = new ContactsVM { IsPartnerAdd = ((PartnerFormVM)DataContext).ChosenPartner.ID,PartnerContactList= ((PartnerFormVM)DataContext).ContactsList };
            PartnersWindow partners = new PartnersWindow { DataContext = contacts, Title = "Elérhetőségek" };
            contacts.LoadPartnersList();
            partners.Show();
        }

        private void ContactRemoveClick(object sender, RoutedEventArgs e)
        {
            var formVM = DataContext as PartnerFormVM;
            if (formVM.SelectedContact != null)
                formVM.RemoveContact();
        }
    }
}
