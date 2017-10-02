using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnersVM : BaseModel
    {
        private string _searchText;

        public ObservableCollection<Partner> PartnersList { get; set; }
        public Partner SelectedParameter { get; set; }
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChange("SearchText"); OnPropertyChange("FilteredPartnersList"); }
        }
        public IEnumerable<Partner> FilteredPartnersList
        {
            get
            {
                if (SearchText == null) return PartnersList;
                return PartnersList.Where(x => x.Name.ToUpper().StartsWith(SearchText.ToUpper()));
            }
        }

        public PartnersVM()
        {
            Context context = new Context();
            PartnersList = new ObservableCollection<Partner>();
            foreach (var item in context.PartnerList)
            {
                PartnersList.Add(new Partner(item));
            }
        }

        internal void RemovePartner()
        {
            using (RegistryModel registry = new RegistryModel())
            {
                var result = registry._switch.Where(x => x.PartnerId == SelectedParameter.ID);
                foreach (var item in result)
                {
                    registry._switch.Remove(item);
                }
                registry.partners.Remove(registry.partners.First(x => x.ID == SelectedParameter.ID));
                registry.SaveChanges();
            }
            //PartnersList.Remove(SelectedParameter);
        }
    }
}

