using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var records = new List<object>();


            Console.WriteLine("Enter the folder to search");             
            var src = Console.ReadLine();                               // no error checking at this point

            Console.WriteLine("Enter the folder to write the CSV");
            var dst = Console.ReadLine();                               // No error checking here either


            Console.WriteLine("Shall we search recursively? True or False");
            var recurse = Console.ReadLine();


            var RecurseYN = GetRecursiveOptions(Convert.ToBoolean(recurse));

            var x = new DirectoryInfo(src).GetFiles("*.*", RecurseYN);              // turn into a list of files

            foreach (var item in x)                                                 // loop through list
            {
                if (item.Length > 10)                                               // Choose file length of at least 10
                {
                    var fs = new FileStream(item.FullName, FileMode.Open);
                    var ba = new byte[4];
                    fs.Read(ba, 0, 4);
                    fs.Close();

                    if (IsPdf(ba))
                    {
                        records.Add(new { FullPath = item.FullName, FileType = "PDF", MD5 = CreateMD5Hash(item.FullName) });
                        }


                    if (IsJpg(ba))
                        {
                            records.Add(new { FullPath = item.FullName, FileType = "JPG", MD5= CreateMD5Hash(item.FullName) });
                        }

                        Console.WriteLine("Done...");
                    }
                }



            using (var writer = new StreamWriter(@"c:\temp\file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }



            
        }



        public static bool IsJpg(byte[] ba)
        {
            if (ba[0] == 255 && ba[1] == 216)
            {
                return true;
            }
            else
                return false;
        }




        
        public static bool IsPdf(byte[] ba)
        {
            if (ba[0] == 37 && ba[1] == 80 && ba[2] == 68 && ba[3] == 70)
            {
                return true;
            }
            else
                return false;
        }




        private static EnumerationOptions GetRecursiveOptions(bool v)
        {
            var foo = new EnumerationOptions();

            if (v)
            {
                foo.RecurseSubdirectories = true;
            }
            else
            {
                foo.RecurseSubdirectories = false;
            }

            return foo;

        }






        public static string CreateMD5Hash(string FileName)
        {
            
            StringBuilder sb = new StringBuilder();                                             // instantiate stringbuilder to hold hash stuff
            MD5 md5 = System.Security.Cryptography.MD5.Create();                                // instantiate obj
            var fileContents = File.ReadAllText(FileName);                                      // get text
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(fileContents);              // get/encode in byte array
            byte[] hashBytes = md5.ComputeHash(inputBytes);                                     // do the hash thingy
            
            for (int i = 0; i < hashBytes.Length; i++)                                          // loop through the byte array
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }




    }
}
