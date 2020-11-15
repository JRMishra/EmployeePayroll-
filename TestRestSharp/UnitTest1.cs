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

        //[TestMethod]
        //public void givenEmployeeList_OnPost_ShouldReturnAddedEmployee()
        //{
        //    RestRequest request = new RestRequest("/employees", Method.POST);

        //    IList<JObject> employees = new List<JObject>();

        //    JObject jObjectbody = new JObject();
        //    jObjectbody.Add("name", "Tony");
        //    jObjectbody.Add("Salary", 18000);
        //    employees.Add(jsonobject);

        //    jObjectbody = new JObject();
        //    jObjectbody.Add("name", "Thanos");
        //    jObjectbody.Add("Salary", 180000);
        //    jsonobject = new JsonParameter("employees", jObjectbody);

        //    //var data = JsonConvert.SerializeObject(employees);
        //    //System.Console.WriteLine(data.Substring(1,data.Length-2));
        //    request.AddOrUpdateParameters(employees);

        //    //act
        //    IRestResponse response = client.Execute(request);
        //    Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
        //    //IList dataResponse = JsonConvert.DeserializeObject<List>(response.Content);

        //    //Assert.AreEqual(2, dataResponse.Count);
        //    //Assert.AreEqual("Thanos", dataResponse[1].name);
        //}

        [TestMethod]
        public void givenEmployeeId_OnPut_ShouldReturnUpdatedEmployee()
        {
            RestRequest request = new RestRequest("/employees/3", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Shiva");
            jObjectbody.Add("Salary", "30000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Shiva", dataResponse.name);
            Assert.AreEqual(30000, dataResponse.salary);
        }
    }
}
