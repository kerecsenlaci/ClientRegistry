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
    /// Interaction logic for PartnersWindow.xaml
    /// </summary>
    public partial class PartnersWindow : Window
    {
        public PartnersWindow()
        {
            InitializeComponent();
        }

        private void PartnerFormClick(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is PartnersVM)
            {
                PartnersVM partnersVM = DataContext as PartnersVM;
                if (partnersVM != null && partnersVM.SelectedPartner != null)
                {
                    PartnerFormVM partnerForm = new PartnerFormVM { ChosenPartner = partnersVM.SelectedPartner, IsEdit = true };
                    PartnerFormWindow formWindow = new PartnerFormWindow { DataContext = partnerForm };
                    partnerForm.ValuesTransmission();
                    formWindow.ShowDialog();
                    
                }
            }
            else
            {
                ContactsVM contactsVM = DataContext as ContactsVM;
                if(contactsVM!=null && contactsVM.SelectedPartner != null && contactsVM.IsPartnerAdd!=null)
                {
                    contactsVM.AddContact();
                    contactsVM.PartnersList.Remove(contactsVM.SelectedPartner);
                    return;
                }
                if(contactsVM!=null && contactsVM.SelectedPartner != null)
                    {
                    ContactFormVM contactForm = new ContactFormVM { ChosenContact = contactsVM.SelectedPartner,IsEdit=true };
                    ContactFormWindow formWindow = new ContactFormWindow { DataContext = contactForm };
                    contactForm.CopyContact();
                    formWindow.ShowDialog();
                }
            }
            
        }

        private void NewPartnerFormClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PartnersVM)
            {
                PartnersVM partnersVM = DataContext as PartnersVM;
                if (partnersVM != null)
                {
                    PartnerFormVM partnerForm = new PartnerFormVM { ChosenPartner = new Partner(), IsEdit = false };
                    PartnerFormWindow formWindow = new PartnerFormWindow { DataContext = partnerForm };
                    partnerForm.ValuesTransmission();
                    formWindow.ShowDialog();
                    partnersVM.RefreshList();
                }
            }
            else
            {
                ContactsVM contactsVM = DataContext as ContactsVM;
                if (contactsVM != null)
                {
                    ContactFormVM contactForm = new ContactFormVM { ChosenContact = new Contact(), IsEdit = false };
                    ContactFormWindow formWindow = new ContactFormWindow { DataContext = contactForm };
                    formWindow.ShowDialog();
                    contactsVM.RefreshList();
                }
            }
        }

        private void RemovePartnerClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PartnersVM)
            {
                PartnersVM partnersVM = DataContext as PartnersVM;
                if (partnersVM != null && partnersVM.SelectedPartner != null)
                {
                    partnersVM.RemovePartner();
                }
            }
            else
            {
                ContactsVM contactsVM = DataContext as ContactsVM;
                var message = "A személy a pertnerektől is törlődni fog!\n\rBiztos hogy törli?";
                if(MessageBox.Show(message,"Figyelmeztetés",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                if (contactsVM != null && contactsVM.SelectedPartner != null)
                {
                    if (!contactsVM.RemoveContact())
                        MessageBox.Show("Nem törölhető mert tulajdonos egy vagy több partnernél!");
                }
            }
        }
    }
}
