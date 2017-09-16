using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class ContactsVM
    {
        public ObservableCollection<Contact> PartnersList { get; set; }
        public Contact SelectedParameter { get; set; }

        public ContactsVM()
        {
            using(RegistryModel registry = new RegistryModel())
                PartnersList = new ObservableCollection<Contact>(registry.contacts.ToList());
        }
    }
}
