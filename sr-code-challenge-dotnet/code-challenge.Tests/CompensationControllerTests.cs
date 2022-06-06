using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {

        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 100000,
                EffectiveDate = new DateTime(2020,1,1)
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            Assert.AreEqual(compensation.EmployeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);

        }

        [TestMethod]
        public void GetCompensation_Returns_CurrentComp()
        {
            // Arrange
            var compensation = new Compensation
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 120000,
                EffectiveDate = new DateTime(2021, 1, 1)
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

  
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
         
            compensation = new Compensation
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 140000,
                EffectiveDate = new DateTime(2022, 1, 1)
            };

            requestContent = new JsonSerialization().ToJson(compensation);

             postRequestTask = _httpClient.PostAsync("api/compensation",
             new StringContent(requestContent, Encoding.UTF8, "application/json"));

            // this enters a future compensation.  
            compensation = new Compensation
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 200000,
                EffectiveDate = new DateTime(DateTime.Now.Year+1, 1, 1)
            };

            requestContent = new JsonSerialization().ToJson(compensation);

            postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));


            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var newcompensation = response.DeserializeContent<Compensation>();
            // compensation should pull the compensation of 1-1-2022
            Assert.AreEqual("16a596ae-edd3-4847-99fe-c4518e82c86f", newcompensation.EmployeeId);
            Assert.AreEqual(140000, newcompensation.Salary);
            Assert.AreEqual(new DateTime(2022, 1, 1), newcompensation.EffectiveDate);
        }


    }
}
