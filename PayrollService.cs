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
            MultiThreadedEmployeeRepo threadedEmployeeRepo = new MultiThreadedEmployeeRepo();
            //employeeRepo.GetAllEmployee();

            //AddNewEmployee(employeeRepo);

            CompareSequentialWithMultiThreading(employeeRepo, threadedEmployeeRepo);

        }

        private static void AddNewEmployee(EmployeeRepo employeeRepo)
        {
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeName = "Dibya";
            employee.Department = "Tech";
            employee.PhoneNumber = "6309807918";
            employee.Address = "02-Khajauli";
            employee.Gender = 'M';
            employee.BasicPay = 110000.00M;
            employee.Deductions = (SqlMoney)1500.00;
            employee.StartDate = employee.StartDate = Convert.ToDateTime("2020-11-03");

            if (employeeRepo.AddEmployee(employee))
                Console.WriteLine("Records added successfully");

            employeeRepo.RemoveEmployee("Satyam", 6);
        }

        private static void CompareSequentialWithMultiThreading(EmployeeRepo employeeRepo, MultiThreadedEmployeeRepo threadedEmployeeRepo)
        {
            List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
            employeeDetailsList.Add(new EmployeeModel("Batik", "9999999999", "Wuhan", "Research", 'M', 100000, new DateTime(2020, 11, 01)));
            employeeDetailsList.Add(new EmployeeModel("Bipasha", "8888112233", "Mumbai", "Media", 'F', 200000, new DateTime(2020, 11, 10)));
            employeeDetailsList.Add(new EmployeeModel("Rohit", "9876541230", "Mumbai", "Entertainment", 'M', 150000, new DateTime(2020, 11, 01)));

            DateTime startDateTime = DateTime.Now;
            employeeRepo.AddMultipleEmployee(employeeDetailsList);
            DateTime endDateTime = DateTime.Now;
            Console.WriteLine("Without Thread : " + (endDateTime - startDateTime));


            startDateTime = DateTime.Now;
            threadedEmployeeRepo.AddMultipleEmployee(employeeDetailsList);
            endDateTime = DateTime.Now;
            Console.WriteLine("With Thread : " + (endDateTime - startDateTime));
        }
    }
}
