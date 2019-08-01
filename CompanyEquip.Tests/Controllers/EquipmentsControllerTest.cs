using CompanyEquip.Controllers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Moq;

namespace CompanyEquip.Tests.Controllers
{
    [TestClass]
    public class EquipmentsControllerTest
    {
        [TestMethod]
        public void TestDetailsView()
        {
            /* 
             * Moq est capable de créer de fausses implémentations à partir d’une abstraction 
             * (interface ou classe abstraite avec le mot-clé  virtual ) 
             */
            var mockService = new Mock<IEquipmentService>();
            var controller = new EquipmentsController(mockService.Object);
            var result = controller.Details(1) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
