using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRegistry.Dal
{
    public class Context
    {
        public List<PartnerTypeDbModel> PartnerTypeList { get; set; }
        public List<PartnerDbModel> PartnerList { get; set; }
        public List<ContactDbModel> ContactList { get; set; }
        public List<CountyDbModel> CountyList { get; set; }
        public List<SwitchDbModel> SwitchList { get; set; }
        public List<UserDbModel> UserList { get; set; }

        public Context()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                PartnerTypeList = registry.partnertype.ToList();
                PartnerList = registry.partners.OrderBy(x=>x.Name).ToList();
                ContactList = registry.contacts.OrderBy(x=>x.Name).ToList();
                UserList = registry.users.OrderBy(x=>x.Name).ToList();
                CountyList = registry.county.ToList();
                SwitchList = registry._switch.ToList();
            }
        }
    }
}
