using Moq;
using PTLab2_api.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLab2_api.Tests.ServiceTests
{
    [TestClass]
    public class PurchaseServiceTests
    {
        private DataSamples dataSamples = new DataSamples();
        private ServiceResponseSamples responseSamples = new ServiceResponseSamples();

        // GetByUser

        [TestMethod]
        public void GetByUser_ShouldReturnPurchaseList()
        {
            // Arrange
            var user = dataSamples.User;
            var purchases = dataSamples.Purchases;
            var products = dataSamples.Products;

            var expectedResponse = responseSamples.PurchaseGetByUserSuccess();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockProductRepository.Setup(p => p.GetAll()).Returns(products);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.GetById(user.Id)).Returns(responseSamples.UserGetByIdSuccess());

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.GetByUser(user.Id);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.Data, response.Data);

            response.Data = null;
            expectedResponse.Data = null;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void GetByUser_ShouldReturnUserNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var purchases = dataSamples.Purchases;
            var products = dataSamples.Products;

            var expectedResponse = responseSamples.PurchaseGetByUserNotFound();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockProductRepository.Setup(p => p.GetAll()).Returns(products);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.GetById(user.Id)).Returns(responseSamples.UserGetByIdUserNotFound());

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.GetByUser(user.Id);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse, response);
        }

        // MakePurchase

        [TestMethod]
        public void MakePurchase_ShouldReturnPurchaseDto()
        {
            // Arrange
            var user = dataSamples.User;
            var products = dataSamples.Products;
            var product = dataSamples.Product;
            var purchaseDto = dataSamples.PurchaseDto;

            var expectedResponse = responseSamples.PurchaseMakePurchaseSuccess();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockProductRepository.Setup(p => p.Add(product));
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.UpdateSale(user.Id)).Returns(responseSamples.UserUpdateSaleSuccess());
            mockProductService.Setup(p => p.GetById(product.Id)).Returns(responseSamples.ProductGetSuccess(product));

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.MakePurchase(user.Id, product.Id, purchaseDto.Address);

            // Assert
            Assert.AreEqual(expectedResponse.Message, response.Message);
        }

        [TestMethod]
        public void MakePurchase_ShouldReturnUserNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var products = dataSamples.Products;
            var product = dataSamples.Product;
            var purchaseDto = dataSamples.PurchaseDto;

            var expectedResponse = responseSamples.PurchaseMakePurchaseUserNotFound();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockProductRepository.Setup(p => p.Add(product));
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.UpdateSale(user.Id)).Returns(responseSamples.UserUpdateSaleUserNotFound());
            mockProductService.Setup(p => p.GetById(product.Id)).Returns(responseSamples.ProductGetSuccess(product));

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.MakePurchase(user.Id, product.Id, purchaseDto.Address);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse.Message, response.Message);
        }

        [TestMethod]
        public void MakePurchase_ShouldReturnProductNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var products = dataSamples.Products;
            var product = dataSamples.Product;
            var purchaseDto = dataSamples.PurchaseDto;

            var expectedResponse = responseSamples.PurchaseMakePurchaseProductNotFound();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockProductRepository.Setup(p => p.Add(product));
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.UpdateSale(user.Id)).Returns(responseSamples.UserUpdateSaleSuccess());
            mockProductService.Setup(p => p.GetById(product.Id)).Returns(responseSamples.ProductGetNotFound());

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.MakePurchase(user.Id, product.Id, purchaseDto.Address);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse.Message, response.Message);
        }

        [TestMethod]
        public void MakePurchase_ShouldReturnPurchaseDtoWithSale()
        {
            // Arrange
            var userDtoSale = dataSamples.UserDtoSale;
            var products = dataSamples.Products;
            var product = dataSamples.Product;
            var purchaseDtoSale = dataSamples.PurchaseDtoSale;

            var expectedResponse = responseSamples.PurchaseMakePurchaseSuccess();

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockUserService = new Mock<IUserService>();
            var mockProductService = new Mock<IProductService>();

            mockProductRepository.Setup(p => p.Add(product));
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);
            mockUserService.Setup(u => u.UpdateSale(userDtoSale.Id)).Returns(responseSamples.UserUpdateSaleSuccessSale());
            mockProductService.Setup(p => p.GetById(product.Id)).Returns(responseSamples.ProductGetSuccess(product));

            var purchaseService = new PurchaseService(mockUnitOfWork.Object, mockUserService.Object, mockProductService.Object);

            // Act
            var response = purchaseService.MakePurchase(userDtoSale.Id, product.Id, purchaseDtoSale.Address);

            // Assert
            Assert.AreEqual(expectedResponse.Message, response.Message);
        }
    }
}
