using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class ContactFormVM
    {
        public Contact ChosenContact { get; set; }
        public Contact BackupContact { get; set; }
        public bool IsEdit { get; set; }

        public ContactFormVM()
        {
           
        }

        public bool ChosenContactValidate(out string message)
        {
            if (string.IsNullOrEmpty(ChosenContact.Name))
            {
                message = "A név nincs kitöltve!";
                return false;
            }
            else if (string.IsNullOrEmpty(ChosenContact.Phone))
            {
                message = "A telefonszám nincs kitöltve!";
                return false;
            }
            else if (string.IsNullOrEmpty(ChosenContact.Email))
            {
                message = "A email cím nincs kitöltve!";
                return false;
            }
            message = "";
            return true;
        }

        internal void SavePartner()
        {
            if (IsEdit)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    var UpdatePartner = registry.contacts.FirstOrDefault(p => p.ID == ChosenContact.ID);
                    UpdatePartner.Name = ChosenContact.Name;
                    UpdatePartner.Phone = ChosenContact.Phone;
                    UpdatePartner.Email = ChosenContact.Email;
                    UpdatePartner.Status = ChosenContact.Status;
                    registry.SaveChanges();
                }
            }  
            else
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.contacts.Add(new ContactDbModel { Name= ChosenContact.Name,Email=ChosenContact.Email,Phone=ChosenContact.Phone,Status=ChosenContact.Status});
                    registry.SaveChanges();
                }
            }
        }

        public bool IsModified()
        {
            return ChosenContact.Name != BackupContact.Name || ChosenContact.Phone != BackupContact.Phone || ChosenContact.Email != BackupContact.Email || ChosenContact.Status != BackupContact.Status;
        }

        public void CopyContact()
        {
            BackupContact = new Contact
            {
                Name = ChosenContact.Name,
                Phone = ChosenContact.Phone,
                Email = ChosenContact.Email,
                Status = ChosenContact.Status
            };
        }

        public void RestoreContact()
        {
            ChosenContact.Name = BackupContact.Name;
            ChosenContact.Phone = BackupContact.Phone;
            ChosenContact.Email = BackupContact.Email;
            ChosenContact.Status = BackupContact.Status;
        }
    }
}
