using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class ContactFormVM
    {
        DataManager context = new DataManager();
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
                context.UpdateContact(ChosenContact.ID, ChosenContact.Name, ChosenContact.Phone, ChosenContact.Email, ChosenContact.Status);
            else
                context.AddContact(new ContactDbModel { Name = ChosenContact.Name, Email = ChosenContact.Email, Phone = ChosenContact.Phone, Status = ChosenContact.Status });
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
