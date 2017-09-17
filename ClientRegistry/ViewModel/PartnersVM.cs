using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnersVM:BaseModel
    {
        private string _searchText;
        
        public ObservableCollection<Partner> PartnersList { get; set; }
        public Partner SelectedParameter { get; set; }
        public string SearchText { get { return _searchText; }
            set { _searchText = value; OnPropertyChange("SearchText"); OnPropertyChange("FilteredPartnersList"); } }
        public IEnumerable<Partner> FilteredPartnersList { get { if (SearchText == null) return PartnersList;
                return PartnersList.Where(x => x.Name.ToUpper().StartsWith(SearchText.ToUpper())); } }

        public PartnersVM()
        {
            using(RegistryModel registry = new RegistryModel())
                PartnersList = new ObservableCollection<Partner>(registry.partners.ToList());
        }
    }
}
