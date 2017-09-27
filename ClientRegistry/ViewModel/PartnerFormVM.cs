﻿using CRegistry.Dal;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace ClientRegistry
{
    class PartnerFormVM:BaseModel
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
        public County SelectedCounty { get { return _selectedCounty; } set { _selectedCounty = value; OnPropertyChange("SelectedCounty"); ChosenPartner.CountyId = value.ID; } }
        public PartnerType SelectedType { get { return _selectedType; } set { _selectedType = value; OnPropertyChange("SelectedType"); ChosenPartner.TypeId = value.ID; } }
        public Contact SelectedOwner { get { return _selectedContact; } set { _selectedContact = value; OnPropertyChange("SelectedContact"); ChosenPartner.OwnerId = value.ID; } }
        public Contact SelectedContact { get; set; }
        public string[] DropFiles { get; set; }

        public PartnerFormVM()
        {
            
        }

        public void ValuesTransmission()
        {
            CountyList = new ObservableCollection<County>();
            PartnerTypeList = new ObservableCollection<PartnerType>();
            OwnersList = new ObservableCollection<Contact>();
            ContactsList = new ObservableCollection<Contact>();

            Context context = new Context();
            foreach (var item in context.CountyList)
            {
                CountyList.Add(new County { ID = item.ID, CountyName = item.CountyName });
                if (item.ID == ChosenPartner.CountyId)
                    SelectedCounty = CountyList.Last();
            }
                
            foreach (var item in context.PartnerTypeList)
            {
                PartnerTypeList.Add(new PartnerType { ID = item.ID, Name = item.Name });
                if (item.ID == ChosenPartner.TypeId)
                    SelectedType = PartnerTypeList.Last();
            }
                
            foreach (var item in context.ContactList)
            {
                if (item.Status == 4)
                    OwnersList.Add(new Contact(item));
                if (item.ID == ChosenPartner.OwnerId)
                    SelectedOwner = OwnersList.Last();
            }

            foreach (var item in context.SwitchList)
                if (item.PartnerId == ChosenPartner.ID)
                    ContactsList.Add(new Contact( context
                        .ContactList.FirstOrDefault(c => c.ID == item.ContactId)));

            if (IsEdit)
                CopyChosenPartner();
        }

        internal void RemoveContact()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                var result = registry._switch.FirstOrDefault(x => x.PartnerId == ChosenPartner.ID && x.ContactId == SelectedContact.ID);
                registry._switch.Remove(result);
                registry.SaveChanges();
            }
            ContactsList.Remove(SelectedContact);
        }

        internal void CopyChosenPartner()
        {
            BackupContact = new Partner();
            BackupContact.Name = ChosenPartner.Name;
            BackupContact.CountyId = ChosenPartner.CountyId;
            BackupContact.OwnerId = ChosenPartner.OwnerId;
            BackupContact.TypeId = ChosenPartner.TypeId;
            BackupContact.ZipCode = ChosenPartner.ZipCode;
            BackupContact.City = ChosenPartner.City;
            BackupContact.Address = ChosenPartner.Address;
        }

        internal void RestoreContact()
        {
            ChosenPartner.Name = BackupContact.Name;
            ChosenPartner.CountyId = BackupContact.CountyId;
            ChosenPartner.OwnerId = BackupContact.OwnerId;
            ChosenPartner.TypeId = BackupContact.TypeId;
            ChosenPartner.ZipCode = BackupContact.ZipCode;
            ChosenPartner.City = BackupContact.City;
            ChosenPartner.Address = BackupContact.Address;
        }

        internal bool IsModified()
        {
            return ChosenPartner.Name != BackupContact.Name ||
            ChosenPartner.CountyId != BackupContact.CountyId ||
            ChosenPartner.OwnerId != BackupContact.OwnerId ||
            ChosenPartner.TypeId != BackupContact.TypeId ||
            ChosenPartner.ZipCode != BackupContact.ZipCode ||
            ChosenPartner.City != BackupContact.City ||
            ChosenPartner.Address != BackupContact.Address;
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
            else if (ChosenPartner.ZipCode.ToString().Length!=4)
            {
                message = "A irányítószám helytelen!";
                return false;
            }
            else if (string.IsNullOrEmpty(ChosenPartner.Name))
            {
                message = "A név üres!";
                return false;
            }
            else if (string.IsNullOrEmpty(ChosenPartner.City))
            {
                message = "A város üres!";
                return false;
            }
            else if (string.IsNullOrEmpty(ChosenPartner.Address))
            {
                message = "A cím üres!";
                return false;
            }
            message = "";
            return true;
        }

        public void SavePartner()
        {
            if (IsEdit)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    var UpdatePartner = registry.partners.Where(p => p.ID == ChosenPartner.ID).FirstOrDefault();
                    UpdatePartner.Name = ChosenPartner.Name;
                    UpdatePartner.CountyId = ChosenPartner.CountyId;
                    UpdatePartner.OwnerId = ChosenPartner.OwnerId;
                    UpdatePartner.TypeId = ChosenPartner.TypeId;
                    UpdatePartner.ZipCode = ChosenPartner.ZipCode;
                    UpdatePartner.City = ChosenPartner.City;
                    UpdatePartner.Address = ChosenPartner.Address;
                    registry.SaveChanges();
                }
            }
            else
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.partners.Add(new PartnerDbModel { Name=ChosenPartner.Name,TypeId=ChosenPartner.TypeId,CountyId=ChosenPartner.CountyId,ZipCode=ChosenPartner.ZipCode,Address=ChosenPartner.Address,City=ChosenPartner.City,OwnerId=ChosenPartner.OwnerId});
                    registry._switch.Add(new SwitchDbModel { PartnerId = ChosenPartner.ID, ContactId = SelectedOwner.ID });
                    registry.SaveChanges();
                }
            }
        }
    }
}
