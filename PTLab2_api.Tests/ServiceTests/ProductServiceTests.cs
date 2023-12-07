using Moq;

namespace PTLab2_api.Tests.ServiceTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private DataSamples dataSamples = new DataSamples();
        private ServiceResponseSamples responseSamples = new ServiceResponseSamples();

        [TestMethod]
        public void GetById_ShouldReturnProduct()
        {
            // Arrange
            var product = dataSamples.Product;

            var expectedResponse = responseSamples.ProductGetSuccess(product);

            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockProductRepository.Setup(u => u.Get(product.Id)).Returns(product);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);

            // Act
            var response = productService.GetById(product.Id);

            // Assert
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void GetById_ShouldReturnNotFound()
        {
            // Arrange
            var product = dataSamples.Product;
            var nullProduct = dataSamples.NullProduct;

            var expectedResponse = responseSamples.ProductGetNotFound();

            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockProductRepository.Setup(u => u.Get(product.Id)).Returns(nullProduct);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);

            // Act
            var response = productService.GetById(product.Id);

            // Assert
            CollectionAssert.AreEqual(response.ErrorMessages, expectedResponse.ErrorMessages);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void GetAll_ShouldReturnProducts()
        {
            // Arrange
            var products = dataSamples.Products;

            var expectedResponse = responseSamples.ProductGetAllSuccess(products);

            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockProductRepository.Setup(u => u.GetAll()).Returns(products);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);

            // Act
            var response = productService.GetAll();

            // Assert
            CollectionAssert.AreEqual(response.Data, expectedResponse.Data);

            response.Data = null;
            expectedResponse.Data = null;
            Assert.AreEqual(response, expectedResponse);
        }

        [TestMethod]
        public void GetAll_ShouldReturnNotFound()
        {
            // Arrange
            var nullProducts = dataSamples.NullProducts;

            var expectedResponse = responseSamples.ProductGetAllNotFound();

            var mockProductRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockProductRepository.Setup(u => u.GetAll()).Returns(nullProducts);
            mockUnitOfWork.Setup(u => u.Products).Returns(mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);

            // Act
            var response = productService.GetAll();

            // Assert
            CollectionAssert.AreEqual(response.Data, expectedResponse.Data);

            response.ErrorMessages = null;
            expectedResponse.ErrorMessages = null;
            response.Data = null;
            expectedResponse.Data = null;
            Assert.AreEqual(response, expectedResponse);
        }
    }
}
