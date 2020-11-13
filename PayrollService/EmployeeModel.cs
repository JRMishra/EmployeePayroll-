using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace EmployeePayrollService
{
    class EmployeeModel
    {
        public EmployeeModel()
        {
        }

        public EmployeeModel(string employeeName, string phoneNumber, string address, string department, char gender, SqlMoney basicPay, DateTime startDate)
        {
            EmployeeName = employeeName;
            PhoneNumber = phoneNumber;
            Address = address;
            Department = department;
            Gender = gender;
            BasicPay = basicPay;
            StartDate = startDate;
        }

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public char Gender { get; set; }
        public SqlMoney BasicPay { get; set; }
        public SqlMoney Deductions { get; set; }
        public SqlMoney TaxablePay { get; set; }
        public SqlMoney Tax { get; set; }
        public SqlMoney NetPay { get; set; }
        public DateTime StartDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
