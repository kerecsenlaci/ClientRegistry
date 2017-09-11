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
        RegistryModel Db = new RegistryModel();
        public ObservableCollection<partnertype> PartnerTypeList { get; set; }
        public partnertype SelectedType { get; set; }
        public PartnerTypeVM()
        {
            PartnerTypeList = new ObservableCollection<partnertype>(Db.partnertype.ToList());
        }
    }
}
