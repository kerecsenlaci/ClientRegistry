using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class PartnerTypeVM:BaseModel
    {
        bool _isEnabled;
        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public IEnumerable<PartnerType> FilteredPartnerTypeList { get { return PartnerTypeList; } }
        public PartnerType SelectedType { get; set; }
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; OnPropertyChange("IsEnabled"); } }

        public PartnerTypeVM()
        {
            IsEnabled = true;
            using (RegistryModel registry = new RegistryModel())
                PartnerTypeList = new ObservableCollection<PartnerType>(registry.partnertype.ToList());
        }

        public bool SavePartnerType()
        {
            if (SelectedType.ValidateType() && SelectedType.ID == 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.partnertype.Add(SelectedType);
                    registry.SaveChanges();
                }
                return true;
            }
            else if(SelectedType.ValidateType() && SelectedType.ID != 0)
            {
                using (RegistryModel registry = new RegistryModel())
                    registry.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
