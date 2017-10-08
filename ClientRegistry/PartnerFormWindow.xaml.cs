using System.IO;
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
            partners.ShowDialog();
        }

        private void ContactRemoveClick(object sender, RoutedEventArgs e)
        {
            var formVM = DataContext as PartnerFormVM;
            if (formVM.SelectedContact != null)
                formVM.RemoveContact();
        }

        private void ListBoxDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var formVM = DataContext as PartnerFormVM;
                formVM.DropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    FileOperations.FileFormatValidate(formVM.DropFiles);
                }
                catch (FileFormatException)
                {
                    MessageBox.Show("Nem megfelelő fájlformátum!");
                    return;
                }
                foreach (var file in formVM.DropFiles)
                {
                    var fileCutting = FileOperations.FileCutting(file);
                    foreach (var item in fileCutting)
                    {
                        var cformVm = new ContactFormVM();
                        var window = new ContactFormWindow() { DataContext = cformVm };
                        FileOperations.ProcessingVcardPeople(item, cformVm.ChosenContact = new Contact());
                        window.ShowDialog();
                    }
                }
            }
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {
            var formVM = DataContext as PartnerFormVM;
            ReportWindow window = new ReportWindow(formVM.ChosenPartner,formVM.SelectedOwner.Name,formVM.ContactsList);
            window.Show();
        }
    }
}
