using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowService_POC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowService_POC.Model;

namespace WindowService_POC.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        [TestMethod()]
        public void GetDataTest()
        {
            //Arrange
            DataService dataService = new DataService();
            //Act
            var result = dataService.GetData();

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void InsertDataTest()
        {
            //Arrange
            DataService dataService = new DataService();
            
            //Act
            var t = Task.Run(() => dataService.GetData());
            t.Wait();
            //Assert
            Assert.IsNotNull(t.Result);
            dataService.InsertData(t.Result);           
        }
    }
}