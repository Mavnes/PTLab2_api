using System.ComponentModel.DataAnnotations;

namespace PTLab2_api.Data.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public DateTime Date {  get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public float UsedPrice { get; set; }

        public Purchase(int id, DateTime date, string address, int productId, int userId, float usedPrice)
        {
            Id = id;
            Date = date;
            Address = address;
            ProductId = productId;
            UserId = userId;
            UsedPrice = usedPrice;
        }
    }
}
