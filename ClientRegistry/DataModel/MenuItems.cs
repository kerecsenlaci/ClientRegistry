using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class MenuItemsPartner
    {
        public string Name { get; set; }
        public ObservableCollection<Partner> Items { get; set; }

        public MenuItemsPartner()
        {
            Items = new ObservableCollection<Partner>();
        }
    }

    public class MenuItemsContact
    {
        public string Name { get; set; }
        public ObservableCollection<Contact> Items { get; set; }

        public MenuItemsContact()
        {
            Items = new ObservableCollection<Contact>();
        }
    }
}
