using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class PartnerType:BaseModel
    {
        int _id;
        string _name;

        public int ID { get { return _id; } set { _id = value; OnPropertyChange("ID"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChange("Name"); } }
        

        public PartnerType()
        {

        }

        public PartnerType(PartnerTypeDbModel typeDbModel)
        {
            ID = typeDbModel.ID;
            Name = typeDbModel.Name;
        }

        public bool ValidateType()
        {
            return !string.IsNullOrEmpty(Name);
        }

    }
}
