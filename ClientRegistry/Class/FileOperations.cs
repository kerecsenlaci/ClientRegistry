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

        public static string[] FileCutting(string file)
        {
            var begin = false;
            var end = false;
            string[] people;

            using (StreamReader sr = new StreamReader(file))
            {
                people = File.ReadAllLines(file);
            }
            return people;
        }

        public static void ProcessingFile(string file, Contact contact)
        {
            string[] lines = File.ReadAllLines(file);
            foreach (var line in lines)
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
    }
}
