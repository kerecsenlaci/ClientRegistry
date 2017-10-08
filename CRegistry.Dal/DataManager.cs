using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRegistry.Dal
{
    public class DataManager
    {
        RegistryModel registry;

        public DataManager()
        {
            
        }


        /*  ****************  Get Database Table   ******************  */

        public IEnumerable<CountyDbModel> GetCounty()
        {
            registry = new RegistryModel();
            return registry.county;
        }

        public IEnumerable<PartnerTypeDbModel> GetPartnerType()
        {
            registry = new RegistryModel();
            return registry.partnertype.OrderBy(x => x.Name);
        }

        public IEnumerable<PartnerDbModel> GetPartner()
        {
            registry = new RegistryModel();
            return registry.partners.OrderBy(x => x.Name);
        }

        public IEnumerable<ContactDbModel> GetContact()
        {
            registry = new RegistryModel();
            return registry.contacts.OrderBy(x => x.Name);
        }

        public IEnumerable<SwitchDbModel> GetSwitch()
        {
            registry = new RegistryModel();
            return registry._switch;
        }

        public IEnumerable<UserDbModel> GetUser()
        {
            registry = new RegistryModel();
            return registry.users.OrderBy(x => x.Name);
        }



        /* ************ PartnerType *********** */
        public void AddPartnerType(string typeName)
        {
            using (registry = new RegistryModel())
            {
                registry.partnertype.Add(new PartnerTypeDbModel { Name = typeName });
                registry.SaveChanges();
            }
        }

        public void UpdatePartnerType(int updateId, string updateName)
        {
            using (registry = new RegistryModel())
            {
                var updateType = registry.partnertype.FirstOrDefault(p => p.ID == updateId);
                updateType.Name = updateName;
                registry.SaveChanges();
            }
        }

        public void RemovePartnerType(int typeId)
        {
            using (registry = new RegistryModel())
            {
                var removeType = registry.partnertype.Remove(registry.partnertype.FirstOrDefault(p => p.ID == typeId));
                registry.SaveChanges();
            }
        }



        /* ************ Partners *********** */
        //public int SumPartnersFromOwner(int ownerId)
        //{
        //    return GetPartner().Count(x => x.OwnerId == ownerId);
        //}

        public void AddPartner(PartnerDbModel partner)
        {
            using (registry = new RegistryModel())
            {
                registry.partners.Add(partner);
                registry.SaveChanges();
            }
        }

        public void UpdatePartner(PartnerDbModel chosenPartner)
        {
            using (registry = new RegistryModel())
            {
                var updatePartner = registry.partners.FirstOrDefault(x => x.ID == chosenPartner.ID);
                updatePartner.Name = chosenPartner.Name;
                updatePartner.CountyId = chosenPartner.CountyId;
                updatePartner.OwnerId = chosenPartner.OwnerId;
                updatePartner.TypeId = chosenPartner.TypeId;
                updatePartner.ZipCode = chosenPartner.ZipCode;
                updatePartner.City = chosenPartner.City;
                updatePartner.Address = chosenPartner.Address;
                registry.SaveChanges();
            }
        }

        public void RemovePartner(int partnerId)
        {
            using (registry = new RegistryModel())
            {
                registry.partners.Remove(registry.partners.SingleOrDefault(x => x.ID == partnerId));
                registry.SaveChanges();
            }
        }



        /* ************ Contacts *********** */
        public void RemoveContact(int contactId)
        {
            using (registry = new RegistryModel())
            {
                registry.contacts.Remove(registry.contacts.SingleOrDefault(x=>x.ID==contactId));
                registry.SaveChanges();
            }
        }

        public void AddContact(ContactDbModel contact)
        {
            using (registry = new RegistryModel())
            {
                registry.contacts.Add(contact);
                registry.SaveChanges();
            }
        }

        public void UpdateContact(int contactId, string contactName, string contactPhone, string contactEmail, int contactStatus)
        {
            using (registry = new RegistryModel())
            {
                var updateContact = registry.contacts.FirstOrDefault(x => x.ID == contactId);
                updateContact.Name = contactName;
                updateContact.Phone = contactPhone;
                updateContact.Email = contactEmail;
                updateContact.Status = contactStatus;
                registry.SaveChanges();
            }
        }



        /* ************ Switch *********** */
        public void RemoveSwitchRecord(int partnerId, int contactId)
        {
            using(registry = new RegistryModel())
            {
                registry._switch.Remove(registry._switch.FirstOrDefault(x => x.PartnerId == partnerId && x.ContactId == contactId));
                registry.SaveChanges();
            }
        }

        public void AddRecordSwitch(int partnerId, int contactId)
        {
            using (registry = new RegistryModel())
            {
                registry._switch.Add(new SwitchDbModel { PartnerId = partnerId, ContactId = contactId });
                registry.SaveChanges();
            }
        }



        /* ************ Users *********** */
        public void AddUser(string userName, string userPassword)
        {
            using (registry = new RegistryModel())
            {
                registry.users.Add(new UserDbModel { Name = userName, Password = userPassword, Status = false });
                registry.SaveChanges();
            }
        }

        public void UpdateUserName(int userId, string userName)
        {
            using (registry = new RegistryModel())
            {
                var updateUser = registry.users.FirstOrDefault(p => p.ID == userId);
                updateUser.Name = userName;
                registry.SaveChanges();
            }
        }

        public void UpdatePassword(int userId, string userPass)
        {
            using (registry = new RegistryModel())
            {
                var updateUser = registry.users.FirstOrDefault(p => p.ID == userId);
                updateUser.Password = userPass;
                registry.SaveChanges();
            }
        }

        public void RemoveUser(int userId)
        {
            using (registry = new RegistryModel())
            {
                var removeType = registry.users.Remove(registry.users.FirstOrDefault(p => p.ID == userId));
                registry.SaveChanges();
            }
        }



        /* ************ Parameret *********** */
        public string GetParameretValue(string paramName)
        {
            using (registry = new RegistryModel())
            {
                return registry.parameters.FirstOrDefault(y => y.ParameterName == paramName).ParameterValue;
            }
        }

    }
}
