using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace PTLab2_api.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public float Price { get; set; }

        public Product(int id, string name, float price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Name == product.Name &&
                   Price == product.Price;
        }
    }
}
