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
        RegistryModel Db = new RegistryModel();
        public ObservableCollection<partners> PartnersList { get; set; }
        public partners Parameter { get; set; }
        public PartnersVM()
        {
            PartnersList = new ObservableCollection<partners>(Db.partners.ToList());
        }
    }
}
