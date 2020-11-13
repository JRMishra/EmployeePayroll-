using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Assert.AreEqual(14, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "\tName: " + item.name + "\tSalary: " + item.salary);
            }
        }

        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Clark");
            jObjectbody.Add("Salary", "15000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Clark", dataResponse.name);
            Assert.AreEqual(15000, dataResponse.salary);

        }

        [TestMethod]
        public void givenEmployeeList_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            request.RequestFormat = DataFormat.Json;

            JObject[] employees = new JObject[2];
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Tony");
            jObjectbody.Add("Salary", 18000);
            employees[0]=jObjectbody;
            
            jObjectbody = new JObject();
            jObjectbody.Add("name", "Thanos");
            jObjectbody.Add("Salary", 180000);
            employees[1]=jObjectbody;

            var data = JsonConvert.SerializeObject(employees);
            request.AddParameter("application/json", ParameterType.HttpHeader);
            request.AddJsonBody(data);
            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee[] dataResponse = JsonConvert.DeserializeObject<Employee[]>(response.Content);
           
            Assert.AreEqual(2, dataResponse.Length);
            Assert.AreEqual("Thanos", dataResponse[1].name);
        }
    }
}
