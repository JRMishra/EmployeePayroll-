using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace EmployeePayrollService
{
    class MultiThreadedEmployeeRepo
    {
        public static string connectionString = @"Data Source=OCAC\SQK2K20JRM;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void AddEmployee(EmployeeModel model)
        {
            SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
            command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
            command.Parameters.AddWithValue("@Address", model.Address);
            command.Parameters.AddWithValue("@Department", model.Department);
            command.Parameters.AddWithValue("@Gender", model.Gender);
            command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
            command.Parameters.AddWithValue("@StartDate", DateTime.Now);
            try
            {
                this.connection.Open();
                var result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public bool AddMultipleEmployee(List<EmployeeModel> employeesList)
        {
            try
            {
                employeesList.ForEach(employeeData =>
                {
                    Thread thread = new Thread(()=>AddEmployee(employeeData));
                    thread.Start();
                });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
