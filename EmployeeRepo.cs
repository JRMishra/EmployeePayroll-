using System;
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
                        
                        Console.WriteLine("\t"+employeeModel.EmployeeID + "\t" + employeeModel.EmployeeName + "\t\t" + employeeModel.BasicPay + "\t" + employeeModel.StartDate);
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
    }
}