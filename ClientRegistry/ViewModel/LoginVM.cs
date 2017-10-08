using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class LoginVM
    {
        DataManager context = new DataManager();
        public User AuthenticateUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginVM()
        {
            if (context.GetParameretValue("NoLogin") == "Yes")
                AuthenticateUser = new User(context.GetUser().First(x => x.ID == -1));    
        }

        public bool Authentication()
        {
            var user = context.GetUser().FirstOrDefault(x => x.Name == UserName && x.Password == Password);
            if (user != null)
            {
                AuthenticateUser = new User(user);
                return true;
            }
            
            return false;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
