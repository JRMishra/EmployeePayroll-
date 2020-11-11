using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                Thread thread = new Thread(() =>
                {
                    if(connection.State==ConnectionState.Closed)
                        this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    Console.WriteLine("\tMulti thread Execution --" + "Name of employee added : " + model.EmployeeName);
                    this.connection.Close();
                });
                thread.Start();
                thread.Join();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool AddMultipleEmployee(List<EmployeeModel> employeesList)
        {
            try
            {
                employeesList.ForEach(employeeData =>
                {
                    Task task = new Task(() => AddEmployee(employeeData));
                    task.Start();
                    task.Wait();
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
