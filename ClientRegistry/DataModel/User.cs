using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class User:BaseModel
    {
        string _name;

        public int ID { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChange("Name"); } }
        public string Password { get; set; }
        public bool Status { get; set; }

        public User()
        {

        }
        public User(UserDbModel userDb)
        {
            ID = userDb.ID;
            Name = userDb.Name;
            Password = userDb.Password;
            Status = userDb.Status;
        }
    }
}
