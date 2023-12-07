using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLab2_api.Tests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        private DataSamples dataSamples = new DataSamples();
        private ServiceResponseSamples responseSamples = new ServiceResponseSamples();

        // GetById

        [TestMethod]
        public void GetById_ShouldReturnUserDto()
        {
            // Arrange
            var user = dataSamples.User;
            var sales = dataSamples.Sales;

            var expectedResponse = responseSamples.UserGetByIdSuccess();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(user);
            mockSaleRepository.Setup(s => s.Get((int)user.SaleId)).Returns(sales.ElementAt((int)user.SaleId));
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.GetById(user.Id);

            // Assert
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void GetById_ShouldReturnUserNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var nullUser = dataSamples.NullUser;
            var sales = dataSamples.Sales;

            var expectedResponse = responseSamples.UserGetByIdUserNotFound();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(nullUser);
            mockSaleRepository.Setup(s => s.Get((int)user.SaleId)).Returns(sales.ElementAt((int)user.SaleId));
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.GetById(user.Id);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void GetById_ShouldReturnSaleNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var nullUser = dataSamples.NullUser;
            var nullSale = dataSamples.NullSale;

            var expectedResponse = responseSamples.UserGetByIdSaleNotFound();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(user);
            mockSaleRepository.Setup(s => s.Get((int)user.SaleId)).Returns(nullSale);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.GetById(user.Id);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse, response);
        }

        // UpdateSale

        [TestMethod]
        public void UpdateSale_ShouldReturnUserDto()
        {
            // Arrange
            var user = dataSamples.User;
            var sales = dataSamples.Sales;
            var purchases = dataSamples.Purchases;
            
            var userUpdated = user;
            userUpdated.TotalExpenses = 1500;

            var expectedResponse = responseSamples.UserUpdateSaleSuccess();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(user);
            mockUserRepository.Setup(u => u.Update(userUpdated));
            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockSaleRepository.Setup(s => s.GetAll()).Returns(sales);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.UpdateSale(user.Id);

            // Assert
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void UpdateSale_ShouldReturnUserNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var nullUser = dataSamples.NullUser;
            var sales = dataSamples.Sales;
            var purchases = dataSamples.Purchases;

            var userUpdated = user;
            userUpdated.TotalExpenses = 1500;

            var expectedResponse = responseSamples.UserUpdateSaleUserNotFound();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(nullUser);
            mockUserRepository.Setup(u => u.Update(userUpdated));
            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockSaleRepository.Setup(s => s.GetAll()).Returns(sales);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.UpdateSale(user.Id);

            // Assert
            CollectionAssert.AreEqual(expectedResponse.ErrorMessages, response.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void UpdateSale_ShouldReturnNotUpdated()
        {
            // Arrange
            var user = dataSamples.User;
            var nullSales = dataSamples.NullSales;
            var purchases = dataSamples.Purchases;

            var userUpdated = user;
            userUpdated.TotalExpenses = 1500;

            var expectedResponse = responseSamples.UserUpdateSaleNotUpdated();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(user);
            mockUserRepository.Setup(u => u.Update(userUpdated));
            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockSaleRepository.Setup(s => s.GetAll()).Returns(nullSales);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.UpdateSale(user.Id);

            // Assert
            response.ErrorMessages = null;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void UpdateSale_ShouldReturnUpdatedSale()
        {
            // Arrange
            var user = dataSamples.UserForSaleUpdate;
            var sales = dataSamples.Sales;
            var purchases = dataSamples.PurchasesForSaleUpdate;

            var userUpdated = user;
            userUpdated.TotalExpenses = 8000;

            var expectedResponse = responseSamples.UserUpdateSaleSuccessForSaleUpdate();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.Get(user.Id)).Returns(user);
            mockUserRepository.Setup(u => u.Update(userUpdated));
            mockPurchaseRepository.Setup(p => p.GetAll()).Returns(purchases);
            mockSaleRepository.Setup(s => s.GetAll()).Returns(sales);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(u => u.Purchases).Returns(mockPurchaseRepository.Object);
            mockUnitOfWork.Setup(u => u.Sales).Returns(mockSaleRepository.Object);

            var userService = new UserService(mockUnitOfWork.Object);

            // Act
            var response = userService.UpdateSale(user.Id);

            // Assert
            Assert.AreEqual(expectedResponse, response);
        }
    }
}
