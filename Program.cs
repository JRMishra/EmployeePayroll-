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

            EmployeeRepo employeeRepo = new EmployeeRepo();
            employeeRepo.GetAllEmployee();

            //EmployeeModel employee = new EmployeeModel();
            //employee.EmployeeName = "Dibya";
            //employee.Department = "Tech";
            //employee.PhoneNumber = "6309807918";
            //employee.Address = "02-Khajauli";
            //employee.Gender = 'M';
            //employee.BasicPay = 110000.00M;
            //employee.Deductions = (SqlMoney)1500.00;
            //employee.StartDate = employee.StartDate = Convert.ToDateTime("2020-11-03");

            //if (employeeRepo.AddEmployee(employee))
            //    Console.WriteLine("Records added successfully");

        }
    }
}
