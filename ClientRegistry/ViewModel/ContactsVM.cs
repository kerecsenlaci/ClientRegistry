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
        RegistryModel Db = new RegistryModel();
        public ObservableCollection<contacts> PartnersList { get; set; }
        public contacts Parameter { get; set; }
        public ContactsVM()
        {
            PartnersList = new ObservableCollection<contacts>(Db.contacts.ToList());
        }
    }
}
