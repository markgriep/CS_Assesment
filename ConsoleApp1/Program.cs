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


        /// <summary>
        /// This is a bare bones MVP with not much error checking.  Works, but needs a bit of refactoring.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var records = new List<object>();


            Console.WriteLine($"{System.Environment.NewLine}Enter below, the folder to search");             
            var SourceFolder = Console.ReadLine();                                                               

            Console.WriteLine($"{System.Environment.NewLine}Enter below, the folder to write the CSV");
            var DestinationFileAndFolder = Console.ReadLine();                               


            Console.WriteLine($"{System.Environment.NewLine}Shall we search recursively? True or False");
            var recurse = Console.ReadLine();


            var RecurseYN = GetRecursiveOptions(Convert.ToBoolean(recurse));

            var x = new DirectoryInfo(SourceFolder).GetFiles("*.*", RecurseYN);              // turn into a list of files

            foreach (var item in x)                                                 // loop through list
            {
                if (item.Length > 10)                                               // Choose file length of at least 10
                {
                    var fs = new FileStream(item.FullName, FileMode.Open);          // TODO.  Refactor and pull these lines into the method.
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



            using (var writer = new StreamWriter(DestinationFileAndFolder))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }



            
        }


        /// <summary>
        /// Given a byte array, return a True if signature corresponds to a JPG
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool IsJpg(byte[] ba)
        {
            if (ba[0] == 255 && ba[1] == 216)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Given a byte array return a true if it matches the signature of a PDF
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static bool IsPdf(byte[] ba)
        {
            if (ba[0] == 37 && ba[1] == 80 && ba[2] == 68 && ba[3] == 70)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Return a enumeation options object that matches the recursive true/False
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Given a filename, "extract" the MD5 hash from it.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string CreateMD5Hash(string FileName)
        {

            StringBuilder sb = new StringBuilder();                                             // instantiate stringbuilder to hold hash stuff
            var fileContents = File.ReadAllBytes(FileName);                                     // get text
            
            byte[] hash;
            using (var md5 = System.Security.Cryptography.MD5.Create()) {
                md5.TransformFinalBlock(fileContents, 0, fileContents.Length);
                hash = md5.Hash;
            }


            for (int i = 0; i < hash.Length; i++)                                          // loop through the byte array...
            {
                sb.Append(hash[i].ToString("X2"));                                          // convert to HEX from DEC
            }
            return sb.ToString();
        }




    }
}
