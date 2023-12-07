using System.ComponentModel.DataAnnotations;

namespace PTLab2_api.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public float? TotalExpenses { get; set; }
        public int? SaleId { get; set; }

        public User(int id, string name, string email, string password, float totalExpenses, int saleId) 
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            TotalExpenses = totalExpenses;
            SaleId = saleId;
        }

        public User(string name, string email, string password)
        {
            Id = 0;
            Name = name;
            Email = email;
            Password = password;
            TotalExpenses = null;
            SaleId = null;
        }
        public User()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            TotalExpenses = null;
            SaleId = null;
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Name == user.Name &&
                   Email == user.Email &&
                   Password == user.Password &&
                   TotalExpenses == user.TotalExpenses &&
                   SaleId == user.SaleId;
        }
    }
}
