using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;
using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportStructureControllerTests
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
        public void ReportStructure_Returns_CorrectCount()
        {

            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f"; // this is John Lennon

            var expectedCount = 4; // per instructions

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedCount, reportingStructure.NumberOfReports);

        }

        [TestMethod]
        public void ReportStructure_Returns_ZeroIfNullReports()
        {

            // Arrange
            var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3"; // this is Paul McCartney
            var expectedCount = 0; 

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedCount, reportingStructure.NumberOfReports);

        }


    }
}
