using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Source Folder");
            var src = Console.ReadLine();

            Console.WriteLine("CSV Destination  Folder");
            var dst = Console.ReadLine();


            Console.WriteLine("Recursive T or F");
            var recurse = Console.ReadLine();


            Console.WriteLine($"S:{src} D:{dst} R:{recurse}");




        }
    }
}
