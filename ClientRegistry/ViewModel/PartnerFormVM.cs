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
        RegistryModel registry = new RegistryModel();
        public partners Partner { get; set; }
        public ObservableCollection<county> CountyList { get; set; }
        public county SelectedCounty { get; set; }
        public ObservableCollection<partnertype> PartnerTypeList { get; set; }
        public partnertype SelectedType { get { return registry.partnertype.FirstOrDefault(ptype => ptype.ID == Partner.TypeId); } set { } }
        public ObservableCollection<contacts> ContactsList { get; set; }
        public contacts SelectedContact { get { return registry.contacts.FirstOrDefault(cont => cont.ID == Partner.OwnerId); } set { } }


        public PartnerFormVM()
        {
            CountyList = new ObservableCollection<county>(registry.county);
            PartnerTypeList = new ObservableCollection<partnertype>(registry.partnertype);
            ContactsList = new ObservableCollection<contacts>(registry.contacts);
            SelectedCounty = registry.county.FirstOrDefault(c => c.ID == Partner.CountyId);
        }
    }
}
