using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRegistry.Dal;

namespace ClientRegistry
{
    class ContactsVM:BaseModel
    {
        private string _searchText;

        public int? IsPartnerAdd { get; set; }
        public ObservableCollection<Contact> PartnersList { get; set; }
        public ObservableCollection<Contact> PartnerContactList { get; set; }
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
            
        }

        public void LoadPartnersList()
        {
            Context context = new Context();
            PartnersList = new ObservableCollection<Contact>();
            if (IsPartnerAdd == null)
                foreach (var item in context.ContactList)
                    PartnersList.Add(new Contact(item));
            else
                foreach (var item in context.ContactList)
                    if (!context.SwitchList.Where(x => x.PartnerId == IsPartnerAdd).ToList().Exists(x => x.ContactId == item.ID && x.PartnerId == IsPartnerAdd))
                        PartnersList.Add(new Contact(item));
        }

        internal void AddContact()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                registry._switch.Add(new SwitchDbModel { PartnerId = (int)IsPartnerAdd, ContactId = SelectedParameter.ID });
                registry.SaveChanges();
            }
            PartnerContactList.Add(SelectedParameter);
        }

        internal bool RemovePartner()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                if(registry.partners.Count(x => x.OwnerId == SelectedParameter.ID)<=1)
                {
                    var result = registry._switch.Where(x => x.ContactId == SelectedParameter.ID);
                    foreach (var item in result)
                    {
                        registry._switch.Remove(item);
                    }
                    registry.contacts.Remove(registry.contacts.First(x => x.ID == SelectedParameter.ID));
                    registry.SaveChanges();
                    PartnersList.Remove(SelectedParameter);
                    return true;
                }
            }
            return false;
        }

        internal void RefreshList()
        {
            PartnersList.Clear();
            Context context = new Context();
            foreach (var item in context.ContactList)
                PartnersList.Add(new Contact(item));
        }
    }
}
