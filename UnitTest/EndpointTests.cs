using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Programming_Exercise.Business;
using Programming_Exercise.Business.File;

namespace UnitTest
{
    [TestClass]
    public class EndpointTests
    {
        private Operation operation = new Operation();

        [TestMethod]
        public void GetEndpointsFromFile()
        {
            operation.ListAllEndPoint();
        }

        [TestMethod]
        public void SearchEndpoints()
        {
            operation.SearchEndPoint(serialNumber: "1111", mainMenu: false);
        }

        [TestMethod]
        public void DeleteEndpoints()
        {
            operation.DeleteEndPoint(serialNumber: "1111");
        }

        [TestMethod]
        public void GetFileEndpoints()
        {
            FileManager fileManager = new FileManager();
            fileManager.GetFileContent();
        }
    }
}
