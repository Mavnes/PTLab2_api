using PTLab2_api.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PTLab2_api.Tests
{
    internal class DataSamples
    {
        // Sale
        public Sale? NullSale = null;

        public List<Sale> Sales = new List<Sale>()
            {
                new Sale(0, 0, 0),
                new Sale(1, 2, 5000),
                new Sale(2, 5, 10000),
                new Sale(3, 10, 50000),
                new Sale(4, 25, 100000),
            };

        public List<Sale>? NullSales = null;

        // User
        public User User = new User(0, "User Name", "user@mail.com", "UserPassword1", 0, 0);
        public User UserForSaleUpdate = new User(2, "User Name", "user@mail.com", "UserPassword1", 0, 0);
        public User? NullUser = null;
        public UserDto UserDto = new UserDto(0, "User Name", "user@mail.com", "UserPassword1", 1500, 0);
        public UserDto UserDtoForSaleUpdate = new UserDto(2, "User Name", "user@mail.com", "UserPassword1", 8000, 2);
        public UserDto UserDtoSale = new UserDto(0, "User Name", "user@mail.com", "UserPassword1", 0, 10);
        public UserDto? NullUserDto = null;

        //Product
        public Product Product = new Product(0, "Product Name", 1000);
        public Product? NullProduct = null;

        public List<Product> Products = new List<Product>()
            {
                new Product(0, "Product 0 Name", 1000),
                new Product(1, "Product 1 Name", 1500),
                new Product(2, "Product 2 Name", 500),
                new Product(3, "Product 3 Name", 3000),
                new Product(4, "Product 4 Name", 2000),
            };

        public List<Product>? NullProducts = null;

        // Purchase
        public Purchase Purchase = new Purchase(0, DateTime.Now, "Adress 1", 0, 0, 1000);
        public Purchase? NullPurchase = null;
        public PurchaseDto PurchaseDto = new PurchaseDto(0, DateTime.Now, "Adress 1", "Product 0 Name", 1000);
        public PurchaseDto PurchaseDtoSale = new PurchaseDto(0, DateTime.Now, "Adress 1", "Product 0 Name", 900);
        public PurchaseDto? NullPurchaseDto = null;

        public List<Purchase> Purchases = new List<Purchase>()
            {
                new Purchase(0, DateTime.Now, "Adress 1", 0, 0, 1000),
                new Purchase(1, DateTime.Now, "Adress 3", 1, 2, 1500),
                new Purchase(2, DateTime.Now, "Adress 1", 2, 0, 500),
                new Purchase(3, DateTime.Now, "Adress 5", 3, 4, 3000),
                new Purchase(4, DateTime.Now, "Adress 2", 4, 1, 2000),
            };

        public List<Purchase> PurchasesForSaleUpdate = new List<Purchase>()
            {
                new Purchase(0, DateTime.Now, "Adress 1", 0, 0, 1000),
                new Purchase(1, DateTime.Now, "Adress 3", 1, 2, 1500),
                new Purchase(2, DateTime.Now, "Adress 1", 2, 0, 500),
                new Purchase(3, DateTime.Now, "Adress 5", 3, 2, 3000),
                new Purchase(4, DateTime.Now, "Adress 2", 4, 2, 2000),
                new Purchase(0, DateTime.Now, "Adress 1", 0, 0, 1000),
                new Purchase(1, DateTime.Now, "Adress 3", 1, 2, 1500),
                new Purchase(2, DateTime.Now, "Adress 1", 2, 0, 500)
            };

        public List<PurchaseDto> UserPurchases = new List<PurchaseDto>()
            {
                new PurchaseDto(0, DateTime.Now, "Adress 1", "Product 0 Name", 1000),
                new PurchaseDto(2, DateTime.Now, "Adress 1", "Product 2 Name", 500)
            };
    }
}
