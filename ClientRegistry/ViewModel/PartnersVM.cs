using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class PartnersVM : BaseModel
    {
        private string _searchText;

        DataManager context = new DataManager();
        public ObservableCollection<Partner> PartnersList { get; set; }
        public Partner SelectedPartner { get; set; }
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
            PartnersList = new ObservableCollection<Partner>();
            foreach (var item in context.GetPartner())
                PartnersList.Add(new Partner(item));
        }

        internal void RemovePartner()
        {
            context.RemovePartner(SelectedPartner.ID);
            PartnersList.Remove(SelectedPartner);
        }

        internal void RefreshList()
        {
            PartnersList.Clear();
            foreach (var item in context.GetPartner())
                PartnersList.Add(new Partner(item));
        }
    }
}

