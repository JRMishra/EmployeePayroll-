using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace EmployeePayrollService
{
    class PayrollService
    {
        public static void Start()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            
            //employeeRepo.GetAllEmployee();

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

            //employeeRepo.RemoveEmployee("Satyam", 6);

            List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
            employeeDetailsList.Add(new EmployeeModel("Bruce", "9999999999", "Melbourn", "HR", 'M', 100, new DateTime(2020, 10, 01)));
            employeeDetailsList.Add(new EmployeeModel("Banner", "8888112233", "Berlin", "HR", 'M', 100, new DateTime(2020, 10, 10)));
            employeeDetailsList.Add(new EmployeeModel("Clark", "9876541230", "Newyork", "HR", 'M', 100, new DateTime(2020, 11, 01)));
            bool result = employeeRepo.AddMultipleEmployee(employeeDetailsList);
            if (result)
                Console.WriteLine("All details in list added successfully...");
        }
    }
}
