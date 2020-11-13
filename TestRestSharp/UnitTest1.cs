using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace TestRestSharp
{
    [TestClass]
    public class TestJsonServerUsingRestSharp
    {
        RestClient client;

        [TestInitialize]
        public void TestMethod1()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse GetEmpList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void onCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = GetEmpList();

            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(13, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "\tName: " + item.name + "\tSalary: " + item.salary);
            }
        }

    }
}
