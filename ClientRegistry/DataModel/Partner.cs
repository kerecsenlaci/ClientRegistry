using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class Partner:BaseModel
    {
        string _name;
        string _city;
        int? _countyId;

        public int ID { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChange("Name"); } }
        public int? TypeId { get; set; }
        public int? CountyId { get { return _countyId; } set { _countyId = value; OnPropertyChange("CountyId"); OnPropertyChange("CityName"); OnPropertyChange("LowerIndex"); } }
        public int ZipCode { get; set; }
        public string City { get { return _city; } set { _city = value; OnPropertyChange("City"); OnPropertyChange("LowerIndex"); } }
        public string Address { get; set; }
        public int OwnerId { get; set; }
        public string LowerIndex { get { return $"megye: {CityName}\t\tváros: {City}"; } }
        string CityName {
            get
            {
                using(RegistryModel registry = new RegistryModel())
                    return registry.county.FirstOrDefault(x => x.ID == CountyId).CountyName;
            }
        }

        public Partner()
        {

        }

        public Partner(PartnerDbModel partnerDb)
        {
            ID = partnerDb.ID;
            Name = partnerDb.Name;
            TypeId = partnerDb.TypeId;
            CountyId = partnerDb.CountyId;
            ZipCode = partnerDb.ZipCode;
            City = partnerDb.City;
            Address = partnerDb.Address;
            OwnerId = partnerDb.OwnerId;
        }
    }
}
