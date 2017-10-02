using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRegistry.Dal;

namespace ClientRegistry
{
    public class Contact:BaseModel
    {
        int _id;
        string _name;
        string _phone;
        string _email;
        int _status;

        public int ID { get { return _id; } set { _id = value; OnPropertyChange("ID"); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChange("Name"); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChange("Phone"); OnPropertyChange("LowerIndex"); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChange("Email"); OnPropertyChange("LowerIndex"); } }
        public int Status { get { return _status; } set { _status = value; OnPropertyChange("Status"); } }
        public string LowerIndex { get { return $"tel.: {Phone}\t\temail: {Email}"; } }

        public Contact()
        {

        }

        public Contact(ContactDbModel contact)
        {
            ID = contact.ID;
            Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Status = contact.Status;
            
        }

        public string GetTelInVcf()
        {
            return Phone.Substring(0, 3) + "-" + Phone.Substring(3, 3) + "-" + Phone.Substring(5);
        }

    }
}
