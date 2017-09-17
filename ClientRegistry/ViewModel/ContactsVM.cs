using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class ContactsVM:BaseModel
    {
        private string _searchText;

       
        public ObservableCollection<Contact> PartnersList { get; set; }
        public Contact SelectedParameter { get; set; }
        public IEnumerable<Contact> FilteredPartnersList
        { get { if (SearchText == null) return PartnersList;
                return PartnersList.Where(x => x.Name.ToUpper().StartsWith(SearchText.ToUpper())); } }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange("SearchText");
                OnPropertyChange("FilteredPartnersList");
            }
        }

        public ContactsVM()
        {
            using(RegistryModel registry = new RegistryModel())
                PartnersList = new ObservableCollection<Contact>(registry.contacts.ToList());
        }
    }
}
