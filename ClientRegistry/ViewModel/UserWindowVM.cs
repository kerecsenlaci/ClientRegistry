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

        DataManager context = new DataManager();
        public ObservableCollection<User> UserList { get; set; }
        public IEnumerable<User> FilteredPartnerTypeList { get { return UserList; } }
        public User SelectedUser { get { return _selectedType; } set { _selectedType = value; OnPropertyChange("SelectedUser"); } }
        public bool IsEnabled { get { return isEnabled; } set { isEnabled = value; OnPropertyChange("IsEnabled"); } }

        public UserWindowVM():base()
        {
            UserList = new ObservableCollection<User>();
            LoadUserList();
        }

        void LoadUserList()
        {
            foreach (var item in context.GetUser())
                if (item.ID > 0)
                    UserList.Add(new User(item));
        }

        public bool SaveUser(string passBox1, string passBox2)
        {
            if (ValidateUser() && ValidatePass(passBox1, passBox2) && SelectedUser.ID == 0)
            {
                context.AddUser(SelectedUser.Name, SelectedUser.Password);
                IsEnabled = false;
                UserList.Clear();
                LoadUserList();
                return true;
            }
            else if (ValidateUser() && SelectedUser.ID != 0)
            {
                if (string.IsNullOrEmpty(passBox1) && string.IsNullOrEmpty(passBox2))
                    context.UpdateUserName(SelectedUser.ID, SelectedUser.Name);
                else
                {
                    if (ValidatePass(passBox1, passBox2))
                    {
                        context.UpdateUserName(SelectedUser.ID, SelectedUser.Name);
                        context.UpdatePassword(SelectedUser.ID, SelectedUser.Password);
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
                context.RemoveUser(SelectedUser.ID);
                UserList.Remove(SelectedUser);
                return true;
            }
            return false;
        }

    }
}
