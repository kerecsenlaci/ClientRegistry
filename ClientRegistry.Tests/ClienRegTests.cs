using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRegistry.Dal;

using System.Security.Cryptography;
using System.Linq;
using System.IO;

namespace ClientRegistry.Tests
{
    [TestClass]
    public class ClienRegTests
    {

        [TestMethod]
        public void ContactFormVM_ChosenContactValidation_Test()
        {
            ContactFormVM contact = new ContactFormVM()
            {
                ChosenContact = new Contact()
                {
                    Name = "Valaki",
                    Email = "",
                    Phone = "06791111111"
                }
            };
            var message = "";
            contact.ChosenContactValidate(out message);
            Assert.AreEqual("A email cím nincs kitöltve!", message);
        }

        [TestMethod]
        public void ContactFormVM_CopyContact_Test()
        {
            ContactFormVM contact = new ContactFormVM()
            {
                ChosenContact = new Contact()
                {
                    Name = "Valaki",
                    Email = "valaki@example.com",
                    Phone = "06791111111"
                }
            };

            contact.CopyContact();
            Assert.AreEqual(contact.ChosenContact.Name, contact.BackupContact.Name);
            Assert.AreEqual(contact.ChosenContact.Phone, contact.BackupContact.Phone);
            Assert.AreEqual(contact.ChosenContact.Email, contact.BackupContact.Email);
        }

        [TestMethod]
        public void ContactFormVM_IsModified_Test()
        {
            ContactFormVM contact = new ContactFormVM()
            {
                ChosenContact = new Contact()
                {
                    Name = "Valaki",
                    Email = "valaki@example.com",
                    Phone = "06791111111"
                }
            };

            contact.CopyContact();
            contact.ChosenContact.Name = "Valaki Más";
            Assert.AreEqual(true, contact.IsModified());
            
        }

        [TestMethod]
        public void LoginVM_GetMd5Hash_Test()
        {
            using (MD5 md5Hash = MD5.Create())
            {
                Assert.AreEqual("e3afed0047b08059d0fada10f400c1e5", LoginVM.GetMd5Hash(md5Hash, "Admin"));
            }
        }

        [TestMethod]
        public void PartnerFormVM_CopyChosenPartner_Test()
        {
            PartnerFormVM formVM = new PartnerFormVM()
            {
                ChosenPartner = new Partner()
                {
                    Name = "Alibaba Bt",
                    Address = "Ady E. utca 875",
                    City = "Bp",
                    OwnerId = 1,
                    CountyId = 1,
                    TypeId = 1,
                    ZipCode = 1111
                }
            };
            formVM.CopyChosenPartner();
            Assert.AreEqual(formVM.ChosenPartner.Name, formVM.BackupContact.Name);
            Assert.AreEqual(formVM.ChosenPartner.Address, formVM.BackupContact.Address);
            Assert.AreEqual(formVM.ChosenPartner.City, formVM.BackupContact.City);
            Assert.AreEqual(formVM.ChosenPartner.ZipCode, formVM.BackupContact.ZipCode);
        }

        [TestMethod]
        public void PartnerFormVM_RestoreContact_Test()
        {
            PartnerFormVM formVM = new PartnerFormVM()
            {
                ChosenPartner = new Partner()
                {
                    Name = "Alibaba Bt",
                    Address = "Ady E. utca 875",
                    City = "Bp",
                    OwnerId = 1,
                    CountyId = 1,
                    TypeId = 1,
                    ZipCode = 1111
                }
            };
            formVM.CopyChosenPartner();

            formVM.ChosenPartner.Name = "Teszt Kft";
            formVM.ChosenPartner.Address = "Teszt utca 40";

            formVM.RestoreContact();
            Assert.AreEqual(formVM.ChosenPartner.Name, formVM.BackupContact.Name);
            Assert.AreEqual(formVM.ChosenPartner.Address, formVM.BackupContact.Address);
        }

        //[TestMethod]
        //public void DataManager_AddPartner_Test()
        //{
        //    DataManager manager = new DataManager();
        //    var count = manager.GetContact().Count();
        //    manager.AddContact(new ContactDbModel { Name = "Ez teszt adat", Phone = "000000", Status = 0, Email = "teszt@adat.hu" });
        //    Assert.AreEqual(count + 1, manager.GetContact().Count());
        //}

        //[TestMethod]
        //public void DataManager_UpdateContact_Test()
        //{
        //    DataManager manager = new DataManager();
        //    manager.UpdateContact(manager.GetContact().Max(x=>x.ID), "Ez teszt adat Módosítva", "000000", "teszt@adat.hu",0);
        //    var result = manager.GetContact().FirstOrDefault(x => x.Name == "Ez teszt adat Módosítva").Name;
        //    Assert.IsNotNull(result);

        //}

        //[TestMethod]
        //public void DataManager_RemoveContact_Test()
        //{
        //    DataManager manager = new DataManager();
        //    manager.RemoveContact(manager.GetContact().FirstOrDefault(x => x.Name == "Ez teszt adat Módosítva").ID);
        //    var result = manager.GetContact().FirstOrDefault(x => x.Name == "Ez teszt adat Módosítva").Name;
        //    Assert.IsNull(result);
        //}

        //[TestMethod]
        //public void DataManager_GetParamValue_Test()
        //{
        //    DataManager manager = new DataManager();
        //    var result = manager.GetParameretValue("NoLogin");
        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void FileOperations_SaveVcard_Test()
        {
            FileOperations.SaveVcard(new Contact { Name = "Teszt Alfréd", Phone = "000000000", Email = "teszt.alfred@example.com" });
            ///TODO Az elérési útnak és a fájlnévnek valósnak kell lenni!!!! 
            Assert.IsTrue(File.Exists(@"C:\Users\Laci\Desktop\TesztVcard.vcf"));
        }

        [TestMethod]
        public void FileOperations_ProcessingVcardPeople_Test()
        {
            var message = "";
            var cForm = new ContactFormVM();
            cForm.ChosenContact = new Contact();
            var list = FileOperations.FileCutting(@"C:\Users\Laci\Desktop\TesztVcard.vcf");
            foreach (var item in list)
            {
                FileOperations.ProcessingVcardPeople(item,cForm.ChosenContact);
            }
            Assert.IsTrue(cForm.ChosenContactValidate(out message));
            Assert.IsTrue(string.IsNullOrEmpty(message));
        }
    }
}
