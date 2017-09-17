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
        public Partner BackupContact { get; set; }
        public bool IsEdit { get; set; }
        public ObservableCollection<County> CountyList { get; set; }
        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public ObservableCollection<Contact> OwnersList { get; set; }
        public ObservableCollection<Contact> ContactsList { get; set; }
        public County SelectedCounty { get { return _selectedCounty; } set { ChosenPartner.CountyId = value.ID; } }
        public PartnerType SelectedType { get { return _selectedType; } set { ChosenPartner.TypeId = value.ID; } }
        public Contact SelectedContact { get { return _selectedContact; } set { ChosenPartner.OwnerId = value.ID; } }

        public PartnerFormVM()
        {
            if (IsEdit)
                BackupContact = ChosenPartner;
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
                OwnersList = new ObservableCollection<Contact>(registry.contacts.Where(c=>c.Status==4).ToList());
                ContactsList = new ObservableCollection<Contact>(registry.contacts);
            }
            if (IsEdit)
                BackupContact = ChosenPartner;
        }

        public bool PartnerValidate(out string message)
        {
            if (ChosenPartner.CountyId == null)
            {
                message = "A megye üres!";
                return false;
            }
            else if(ChosenPartner.OwnerId == 0)
            {
                message = "A tulajdonos üres!";
                return false;
            }
            else if (ChosenPartner.TypeId == 0)
            {
                message = "A tipus üres!";
                return false;
            }
            message = "";
            return true;
        }

        public void SavePartner()
        {
            if (IsEdit)
                using (RegistryModel registry = new RegistryModel())
                {
                    var UpdatePartner = registry.partners.Where(p => p.ID == ChosenPartner.ID).FirstOrDefault();
                    UpdatePartner.CountyId = ChosenPartner.CountyId;
                    registry.SaveChanges();
                }
            else
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.partners.Add(ChosenPartner);
                    registry.SaveChanges();
                }
            }
            

        }
    }
}
