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
                if (partnersVM != null && partnersVM.SelectedParameter != null)
                {
                    PartnerFormVM partnerForm = new PartnerFormVM { ChosenPartner = partnersVM.SelectedParameter, IsEdit = true };
                    PartnerFormWindow formWindow = new PartnerFormWindow { DataContext = partnerForm };
                    partnerForm.ValuesTransmission();
                    formWindow.ShowDialog();
                }
            }
            else
            {
                ContactsVM contactsVM = DataContext as ContactsVM;
                if(contactsVM!=null && contactsVM.SelectedParameter != null && contactsVM.IsPartnerAdd!=null)
                {
                    contactsVM.AddContact();
                    contactsVM.PartnersList.Remove(contactsVM.SelectedParameter);
                    return;
                }
                if(contactsVM!=null && contactsVM.SelectedParameter != null)
                    {
                    ContactFormVM contactForm = new ContactFormVM { ChosenContact = contactsVM.SelectedParameter,IsEdit=true };
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
                }
            }
        }

        private void RemovePartnerClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PartnersVM)
            {
                PartnersVM partnersVM = DataContext as PartnersVM;
                if (partnersVM != null && partnersVM.SelectedParameter != null)
                {
                    partnersVM.RemovePartner();
                }
            }
            else
            {
                ContactsVM contactsVM = DataContext as ContactsVM;
                if (contactsVM != null && contactsVM.SelectedParameter != null)
                {
                    contactsVM.RemovePartner();
                }
            }
        }
    }
}
