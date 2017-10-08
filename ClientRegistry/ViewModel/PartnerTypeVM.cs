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

        DataManager context = new DataManager();
        public ObservableCollection<PartnerType> PartnerTypeList { get; set; }
        public IEnumerable<PartnerType> FilteredPartnerTypeList { get { return PartnerTypeList; } }
        public PartnerType SelectedType { get { return _selectedType; } set { _selectedType = value; OnPropertyChange("SelectedType"); } }
        public bool IsEnabled { get { return isEnabled; } set { isEnabled = value; OnPropertyChange("IsEnabled"); } }

        public PartnerTypeVM():base()
        {
            PartnerTypeList = new ObservableCollection<PartnerType>();
            LoadTypeList();
        }

        void LoadTypeList()
        {
            foreach (var item in context.GetPartnerType())
            {
                PartnerTypeList.Add(new PartnerType(item));
            }
        }

        public bool SavePartnerType()
        {
            if (SelectedType.ValidateType() && SelectedType.ID == 0)
            {
                context.AddPartnerType(SelectedType.Name);
                IsEnabled = false;
                PartnerTypeList.Clear();
                LoadTypeList();
                return true;
            }
            else if (SelectedType.ValidateType() && SelectedType.ID != 0)
            {
                context.UpdatePartnerType(SelectedType.ID, SelectedType.Name);
                IsEnabled = false;
                return true;
            }

            return false;
        }

        public bool RemovePartnerType()
        {
            if (SelectedType != null && SelectedType.ID != 0 && context.GetPartner().Where(x=>x.TypeId==SelectedType.ID).Count()==0)
            {
                context.RemovePartnerType(SelectedType.ID);
                PartnerTypeList.Remove(SelectedType);
                return true;
            }
            return false;
        }
    }
}
