using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnerFormVM
    {
        County _selectedCounty;
        PartnerType _selectedType;
        Contact _selectedContact;

        public Partner ChosenPartner { get; set; }
        public ObservableCollection<County> CountyList { get; set; }
        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public ObservableCollection<Contact> ContactsList { get; set; }
        public County SelectedCounty { get { return _selectedCounty; } set { } }
        public PartnerType SelectedType { get { return _selectedType; } set { } }
        public Contact SelectedContact { get { return _selectedContact; } set { } }

        public PartnerFormVM()
        {

        }

        public void ValuesTransmission()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                _selectedCounty = registry.county.FirstOrDefault(c => c.ID == ChosenPartner.CountyId);
                _selectedContact = registry.contacts.FirstOrDefault(cont => cont.ID == ChosenPartner.OwnerId);
                _selectedType = registry.partnertype.FirstOrDefault(ptype => ptype.ID == ChosenPartner.TypeId);
                CountyList = new ObservableCollection<County>(registry.county);
                PartnerTypeList = new ObservableCollection<PartnerType>(registry.partnertype);
                ContactsList = new ObservableCollection<Contact>(registry.contacts);
            }
        }
    }
}
