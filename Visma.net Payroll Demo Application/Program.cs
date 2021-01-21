using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Visma.net_Payroll_Demo_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Visma.net Payroll Demo Application");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Getting the number of employees...");

            PayrollAPI api = new PayrollAPI();

            var count = api.GetNumberOfEmployees().GetAwaiter().GetResult();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The number of employees is: {0}", count);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
