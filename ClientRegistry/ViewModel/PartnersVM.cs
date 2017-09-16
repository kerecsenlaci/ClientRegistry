using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnersVM
    {
        public ObservableCollection<Partner> PartnersList { get; set; }
        public Partner SelectedParameter { get; set; }

        public PartnersVM()
        {
            using(RegistryModel registry = new RegistryModel())
                PartnersList = new ObservableCollection<Partner>(registry.partners.ToList());
        }
    }
}
