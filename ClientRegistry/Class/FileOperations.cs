using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public static class FileOperations
    {
        public static void FileFormatValidate(string[] files)
        {
            foreach (var item in files)
            {
                if (Path.GetExtension(item) != ".vcf")
                {
                    throw new FileFormatException(); 
                }
            }
        }

        public static List<string[]> FileCutting(string file)
        {
            List<string[]> PersonList = new List<string[]>();

            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    var lineBegin = sr.ReadLine();
                    if(lineBegin == "BEGIN:VCARD")
                    {
                        PersonList.Add(new string[] { "","",""});
                        var line = "";
                        for(var i=0; line != "END:VCARD";)
                        {
                            line = sr.ReadLine();
                            if (line.StartsWith("FN")|| line.StartsWith("TEL") || line.StartsWith("EMAIL"))
                            {
                                PersonList.Last()[i] = line;
                                i++;
                            }
                                
                        }
                    }
                }
            }
            return PersonList;
        }

        public static void ProcessingVcardPeople(string[] peopleVcard, Contact contact)
        {
            
            foreach (var line in peopleVcard)
            {
                if (line.StartsWith("FN"))
                {
                    var split = line.Split(':');
                    contact.Name = split[1];
                }
                if (line.StartsWith("TEL"))
                {
                    var split = line.Split(':');
                    contact.Phone = split[1];
                }
                if (line.StartsWith("EMAIL"))
                {
                    var split = line.Split(':');
                    contact.Email = split[1];
                }

            }
        }

        public static void SaveVcard(Contact contact)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Névjegy fájl (*.vcf)|*.vcf";
            if (saveFileDialog.ShowDialog() == true)
                using (Stream myStream = saveFileDialog.OpenFile())
                {
                    using (StreamWriter sw = new StreamWriter(myStream, Encoding.Default))
                    {
                        sw.WriteLine("BEGIN:VCARD");
                        sw.WriteLine("VERSION:2.1");
                        sw.WriteLine($"FN:{contact.Name}");
                        sw.WriteLine($"TEL;CELL:{contact.GetTelInVcf()}");
                        sw.WriteLine($"EMAIL:{contact.Email}");
                        sw.WriteLine("END:VCARD");
                    }
                }

        }
    }
}
