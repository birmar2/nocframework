using Moq;
using NUnit.Framework;
using Common;
using DataLayer.Interfaces;
using Common.Interfaces;
using Common.Models;
using BusinessLayer.Interfaces;
using BusinessLayer;

namespace Test.BusinessLayer
{
    [TestFixture]
    public class FrameWorkTest
    {
        private Mock<IDataService> dataService;
        private Mock<ILogService> logService;
        private Mock<IErrorService> errorService;
        private Mock<IUserService> userService;

        [SetUp]
        public void SetUp()
        {
            dataService = new Mock<IDataService>();
            logService = new Mock<ILogService>();
            errorService = new Mock<IErrorService>();
            userService = new Mock<IUserService>();
        }
        [Test]
        public void Add_Item_ReturnItem()
        {
            //Arrange
            IItemActive item = new ItemActive();
            item.DeviceName = "akármi";
            IFrameWork testObject = new FrameWork(dataService.Object, logService.Object, errorService.Object, userService.Object);

            //Act
            bool ok = testObject.AddItemActive(item);
            var result = testObject.GetItemActive(item.Id);

            //Assert
            Assert.That(result, Is.EqualTo(item));
        }
        [Test]
        public void Change_ItemName()
        {
            //Arrange
            IItemActive item = new ItemActive();
            item.DeviceName = "akarmi";
            IFrameWork testObject = new FrameWork(dataService.Object, logService.Object, errorService.Object, userService.Object);

            //Act
            bool ok = testObject.AddItemActive(item);
            IItemActive change = testObject.GetItemActive(item.Id);
            change.DeviceName = "zsiraf";
            IItemActive result = testObject.GetItemActive(item.Id);


            //Assert
            Assert.That(result.DeviceName, Is.EqualTo("zsiraf"));
        }

    }
}
