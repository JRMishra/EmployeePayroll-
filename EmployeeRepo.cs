using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeePayrollService
{
    internal class EmployeeRepo
    {
        public static string connectionString = @"Data Source=OCAC\SQK2K20JRM;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                string query = @"Select * from employee_payroll;";
                SqlCommand cmd = new SqlCommand(query, this.connection);
                this.connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Console.WriteLine("\tID\tNAME\t\tSALARY\t\tSTART DATE");
                    Console.WriteLine("\t--\t----\t\t------\t\t----------");
                    while (dr.Read())
                    {
                        employeeModel.EmployeeID = dr.GetInt32(0);
                        employeeModel.EmployeeName = dr.GetString(1);
                        employeeModel.BasicPay = dr.GetSqlMoney(2);
                        employeeModel.StartDate = dr.GetDateTime(3);

                        Console.WriteLine("\t" + employeeModel.EmployeeID + "\t" + employeeModel.EmployeeName + "\t\t" + employeeModel.BasicPay + "\t" + employeeModel.StartDate);
                        Console.WriteLine("\t------------------------------------------------------------");
                    }
                }
                else
                {
                    System.Console.WriteLine("No data found");
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddEmployee(EmployeeModel model)
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
                Console.WriteLine("Sequential Execution --\t"+"Name of employee added : " + model.EmployeeName);
                if (result != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }

        public bool RemoveEmployee(string name,int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("delete from employee_payroll where Name = @empname and id = @empId", connection);
                    command.Parameters.AddWithValue("@empname", name);
                    command.Parameters.AddWithValue("@empId", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result + " rows deleted...");
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AddMultipleEmployee(List<EmployeeModel> employeesList)
        {
            try
            {
                employeesList.ForEach(employeeData =>
                {
                    AddEmployee(employeeData);
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