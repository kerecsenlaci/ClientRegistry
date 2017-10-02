using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    class UserWindowVM:BaseModel
    {
        bool isEnabled;
        User _selectedType;

        public ObservableCollection<User> UserList { get; set; }
        public IEnumerable<User> FilteredPartnerTypeList { get { return UserList; } }
        public User SelectedUser { get { return _selectedType; } set { _selectedType = value; OnPropertyChange("SelectedUser"); } }
        public bool IsEnabled { get { return isEnabled; } set { isEnabled = value; OnPropertyChange("IsEnabled"); } }

        public UserWindowVM():base()
        {
            //IsEnabled = true;
            Context context = new Context();
            UserList = new ObservableCollection<User>();
            foreach (var item in context.UserList)
            {
                if (item.ID > 0)
                    UserList.Add(new User(item));
            }
        }

        public bool SaveUser(string passBox1, string passBox2)
        {
            
            if (ValidateUser() && ValidatePass(passBox1, passBox2) && SelectedUser.ID == 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    registry.users.Add(new UserDbModel { Name = SelectedUser.Name, Password= SelectedUser.Password, Status=false });
                    registry.SaveChanges();
                }
                IsEnabled = false;
                return true;
            }
            else if (ValidateUser() && SelectedUser.ID != 0)
            {
                if (string.IsNullOrEmpty(passBox1) && string.IsNullOrEmpty(passBox2))
                {
                    using (RegistryModel registry = new RegistryModel())
                    {
                        var updateType = registry.users.FirstOrDefault(p => p.ID == SelectedUser.ID);
                        updateType.Name = SelectedUser.Name;
                        registry.SaveChanges();
                    }
                }
                else
                {
                    if (ValidatePass(passBox1, passBox2))
                    {
                        using (RegistryModel registry = new RegistryModel())
                        {
                            var updateUser = registry.users.FirstOrDefault(p => p.ID == SelectedUser.ID);
                            updateUser.Name = SelectedUser.Name;
                            updateUser.Password = SelectedUser.Password;
                            registry.SaveChanges();
                        }
                    }
                }
                IsEnabled = false;
                return true;
            }

            return false;
        }

        bool ValidatePass(string passBox1, string passBox2)
        {
            if (string.IsNullOrEmpty(passBox1) && string.IsNullOrEmpty(passBox2))
                return false;
            using (MD5 md5Hash = MD5.Create())
            {
                passBox1 = LoginVM.GetMd5Hash(md5Hash, passBox1);
                passBox2 = LoginVM.GetMd5Hash(md5Hash, passBox2);
                if (passBox1 != passBox2)
                    return false;
                SelectedUser.Password = passBox1;
                return true;
            }
        }

        public bool ValidateUser()
        {
            return !string.IsNullOrEmpty(SelectedUser.Name);
        }

        public bool RemoveUser()
        {
            if (SelectedUser != null && SelectedUser.ID != 0)
            {
                using (RegistryModel registry = new RegistryModel())
                {
                    var removeType = registry.users.Remove(registry.users.FirstOrDefault(p => p.ID == SelectedUser.ID));
                    registry.SaveChanges();
                }
                return true;
            }
            return false;
        }

    }
}
