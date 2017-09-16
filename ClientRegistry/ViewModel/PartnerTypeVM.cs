using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnerTypeVM
    {
        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public PartnerType SelectedType { get; set; }

        public PartnerTypeVM()
        {
            using(RegistryModel registry = new RegistryModel())
                PartnerTypeList = new ObservableCollection<PartnerType>(registry.partnertype.ToList());
        }
    }
}
