using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientRegistry
{
    public class QueryPartnerVM
    {
        public List<MenuItemsPartner> Querry { get; set; }

        public QueryPartnerVM(string menuHeader)
        {
            switch (menuHeader)
            {
                case "Partnerek megyék szerint":
                    PartnerByCounty();
                    break;
                case "Partnerek tipus szerint":
                    PartnerByType();
                    break;
                default:
                    break;
            }
        }

        private void PartnerByType()
        {
            Querry = new List<MenuItemsPartner>();
            Context context = new Context();
            foreach (var item in context.PartnerTypeList)
            {
                Querry.Add(new MenuItemsPartner() { Name = item.Name });
                var lastElement = Querry.Last();
                foreach (var partner in context.PartnerList)
                {
                    if (partner.TypeId == item.ID)
                        lastElement.Items.Add(new Partner(partner));
                }
                if (lastElement.Items.Count == 0)
                    Querry.Remove(lastElement);
            }
        }

        private void PartnerByCounty()
        {
            Querry = new List<MenuItemsPartner>();
            Context context = new Context();
            foreach (var item in context.CountyList)
            {
                Querry.Add(new MenuItemsPartner() { Name = item.CountyName });
                var lastElement = Querry.Last();
                foreach (var partner in context.PartnerList)
                {
                    if (partner.CountyId == item.ID)
                        lastElement.Items.Add(new Partner(partner));
                }
                if (lastElement.Items.Count == 0)
                    Querry.Remove(lastElement);
            }
        }
    }



    public class QueryContactVM
    {
        public List<MenuItemsContact> Querry { get; set; }

        public QueryContactVM(string menuHeader)
        {
            switch (menuHeader)
            {
                case "Tulajdonosok listája":
                    OwnersList();
                    break;
                case "Alkalmazottak listája":
                    EmployeesList();
                    break;
                case "Több üzlettel rendelkező tulajdonosok":
                    MoreBusiness();
                    break;
                default:
                    break;
            }
        }

        private void MoreBusiness()
        {
            Querry = new List<MenuItemsContact>();
            Context context = new Context();
            Querry.Add(new MenuItemsContact() { Name = "Több üzlettel rendelkező tulajdonosok" });
            foreach (var partner in context.ContactList)
            {
                if (partner.Status == 4 && context.SwitchList.Count(x=>x.ContactId==partner.ID)>1)
                    Querry[0].Items.Add(new Contact(partner));
            }
        }

        private void EmployeesList()
        {
            Querry = new List<MenuItemsContact>();
            Context context = new Context();
            Querry.Add(new MenuItemsContact() { Name = "Alkalmazottak" });
            foreach (var partner in context.ContactList)
            {
                if (partner.Status != 4)
                    Querry[0].Items.Add(new Contact(partner));
            }
        }

        private void OwnersList()
        {
            Querry = new List<MenuItemsContact>();
            Context context = new Context();
            Querry.Add(new MenuItemsContact() { Name = "Tulajdonosok"});
            foreach (var partner in context.ContactList)
            {
                if (partner.Status == 4)
                    Querry[0].Items.Add(new Contact(partner));
            }
        }
    }
}

