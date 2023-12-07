using Moq;
using PTLab2_api.Data.DTO;

namespace PTLab2_api.Tests.ServiceTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private DataSamples dataSamples = new DataSamples();
        private ServiceResponseSamples responseSamples = new ServiceResponseSamples();

        // Register

        [TestMethod]
        public void Register_ShouldCreateAccount()
        {
            // Arrange
            var user = dataSamples.User;
            var nullUser = dataSamples.NullUser;
            var sales = dataSamples.Sales;

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                TotalExpenses = user.TotalExpenses,
                Sale = sales.ElementAt(0).Value
            };

            var expectedResponse = responseSamples.RegisterSuccess(userDto);

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.GetUserByEmail(user.Email)).Returns(nullUser);
            mockUserRepository.Setup(u => u.Add(user));
            mockSaleRepository.Setup(s => s.GetBaseSale()).Returns(sales.ElementAt(0));
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(s => s.Sales).Returns(mockSaleRepository.Object);

            var authService = new AuthService(mockUnitOfWork.Object);

            // Act
            var response = authService.Register(user.Name, user.Email, user.Password);

            // Assert
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void Register_ShouldReturnEmailAlreadyExists()
        {
            // Arrange
            var user = dataSamples.User;
            var sales = dataSamples.Sales;

            var expectedResponse = responseSamples.RegisterEmailAlreadyExists();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.GetUserByEmail(user.Email)).Returns(user);
            mockUserRepository.Setup(u => u.Add(user));
            mockSaleRepository.Setup(s => s.GetAll()).Returns(sales);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(s => s.Sales).Returns(mockSaleRepository.Object);

            var authService = new AuthService(mockUnitOfWork.Object);

            // Act
            var response = authService.Register(user.Name, user.Email, user.Password);

            // Assert
            CollectionAssert.AreEqual(response.ErrorMessages, expectedResponse.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(response, expectedResponse);
        }

        // Login

        [TestMethod]
        public void Login_ShouldLogin()
        {
            // Arrange
            var user = dataSamples.User;
            var sales = dataSamples.Sales;

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                TotalExpenses = user.TotalExpenses,
                Sale = sales.ElementAt(0).Value
            };

            var expectedResponse = responseSamples.LoginSuccess(userDto);

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.GetUserByEmail(user.Email)).Returns(user);
            mockSaleRepository.Setup(s => s.Get(0)).Returns(sales.ElementAt(0));
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(s => s.Sales).Returns(mockSaleRepository.Object);

            var authService = new AuthService(mockUnitOfWork.Object);

            // Act
            var response = authService.Login(user.Email, user.Password);

            // Assert
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void Login_ShouldReturnUserNotFound()
        {
            // Arrange
            var user = dataSamples.User;
            var nullUser = dataSamples.NullUser;

            var expectedResponse = responseSamples.LoginUserNotFound();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.GetUserByEmail(user.Email)).Returns(nullUser);
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);

            var authService = new AuthService(mockUnitOfWork.Object);

            // Act
            var response = authService.Login(user.Email, user.Password);

            // Assert
            CollectionAssert.AreEqual(response.ErrorMessages, expectedResponse.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void Login_ShouldReturnWrongPassword()
        {
            // Arrange
            var user = dataSamples.User;
            var sales = dataSamples.Sales;

            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                TotalExpenses = user.TotalExpenses,
                Sale = sales.ElementAt(0).Value
            };

            var expectedResponse = responseSamples.LoginWrongPassword();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockSaleRepository = new Mock<ISaleRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(u => u.GetUserByEmail(user.Email)).Returns(user);
            mockSaleRepository.Setup(s => s.Get(0)).Returns(sales.ElementAt(0));
            mockUnitOfWork.Setup(u => u.Users).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(s => s.Sales).Returns(mockSaleRepository.Object);

            var authService = new AuthService(mockUnitOfWork.Object);

            // Act
            var response = authService.Login(user.Email, "WrongPassword");

            // Assert
            CollectionAssert.AreEqual(response.ErrorMessages, expectedResponse.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(response, expectedResponse);
        }
    }
}
