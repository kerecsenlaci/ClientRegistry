using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientRegistry
{
    public class PartnerTypeVM:BaseModel
    {
        bool isEnabled;
        PartnerType _selectedType;

        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public IEnumerable<PartnerType> FilteredPartnerTypeList { get { return PartnerTypeList; } }
        public PartnerType SelectedType { get { return _selectedType; } set { _selectedType = value; OnPropertyChange("SelectedType"); } }
        public bool IsEnabled { get { return isEnabled; } set { isEnabled = value; OnPropertyChange("IsEnabled"); } }

        public PartnerTypeVM():base()
        {
            //IsEnabled = true;
            Context context = new Context();
            PartnerTypeList = new ObservableCollection<PartnerType>();
            foreach (var item in context.PartnerTypeList)
            {
                PartnerTypeList.Add(new PartnerType(item));
            }
        }

        public bool SavePartnerType()
        {
            if (SelectedType.ValidateType() && SelectedType.ID == 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.partnertype.Add(new PartnerTypeDbModel { Name=SelectedType.Name});
                    registry.SaveChanges();
                }
                IsEnabled = false;
                return true;
            }
            else if(SelectedType.ValidateType() && SelectedType.ID != 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    var updateType = registry.partnertype.FirstOrDefault(p => p.ID == SelectedType.ID);
                    updateType.Name = SelectedType.Name;
                    registry.SaveChanges();
                }
                IsEnabled = false;
                return true;
            }
            
            return false;
        }

        public bool RemovePartnerType()
        {
            if (SelectedType != null && SelectedType.ID != 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    var removeType = registry.partnertype.Remove(registry.partnertype.FirstOrDefault(p => p.ID == SelectedType.ID));
                    registry.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
