using System;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter the folder to search");             
            var src = Console.ReadLine();                               // no error checking at this point

            Console.WriteLine("Enter the folder to write the CSV");
            var dst = Console.ReadLine();                               // No error checking here either


            Console.WriteLine("Shall we search recursively? True or False");
            var recurse = Console.ReadLine();


            src = @"c:\temp";
            recurse = "True";

            var RecurseYN = GetRecursiveOptions(Convert.ToBoolean(recurse));


            var x = new DirectoryInfo(src).GetFiles("*.*", RecurseYN);              // turn into a list of files


            foreach (var item in x)                                                 // loop through list
            {
                //Console.WriteLine(item.FullName);
                if (item.Length > 10)
                {

               
                   

                    var fs = new FileStream(item.FullName, FileMode.Open);

                    var ba = new byte[4];

                    fs.Read(ba, 0, 4);


                    if (IsPdf(ba))
                    {
                        Console.WriteLine($"IS PDF: {item.FullName}");
                    }


                       if (IsJpg(ba))
                    {
                        Console.WriteLine($"IS JPEG: {item.FullName}");
                    }





                    //foreach (var vv in ba.ToList())
                    //{
                    //    Console.WriteLine(vv.ToString());
                    //}





                    //for (int i = 0; i < 4; i++)
                    //{
                    //    int a = Convert.ToInt32(hx.GetValue(i));

                    //    Console.WriteLine(a);

                    //}






                    Console.WriteLine("-================");
                }
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








        // Loop through each dir (maybe recursive)


        // IS PDF

        // IS JPG

        // Get MD5 hash of file

        // Write PDF




    }
}
