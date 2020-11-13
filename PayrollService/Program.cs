using System;
using System.Data.SqlTypes;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tWelcome to Employee Payroll Service Program");
            Console.WriteLine("\t===========================================\n");

            PayrollService.Start();
            Console.ReadKey();
        }
    }
}
