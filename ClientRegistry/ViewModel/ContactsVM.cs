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

        DataManager context = new DataManager();
        public int? IsPartnerAdd { get; set; }
        public ObservableCollection<Contact> PartnersList { get; set; }
        public ObservableCollection<Contact> PartnerContactList { get; set; }
        public Contact SelectedPartner { get; set; }
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
            PartnersList = new ObservableCollection<Contact>();
            if (IsPartnerAdd == null)
                foreach (var item in context.GetContact())
                    PartnersList.Add(new Contact(item));
            else
                foreach (var item in context.GetContact())
                    if (!context.GetSwitch().Where(x => x.PartnerId == IsPartnerAdd).ToList().Exists(x => x.ContactId == item.ID && x.PartnerId == IsPartnerAdd))
                        PartnersList.Add(new Contact(item));
        }

        internal void AddContact()
        {
            context.AddRecordSwitch((int)IsPartnerAdd, SelectedPartner.ID);
            PartnerContactList.Add(SelectedPartner);
        }

        internal bool RemoveContact()
        {
            if (context.GetPartner().Count(x => x.OwnerId == SelectedPartner.ID) == 0)
            {
                context.RemoveContact(SelectedPartner.ID);
                PartnersList.Remove(SelectedPartner);
                return true;
            }
            return false;
        }

        internal void RefreshList()
        {
            PartnersList.Clear();
            foreach (var item in context.GetContact())
                PartnersList.Add(new Contact(item));
        }
    }
}
