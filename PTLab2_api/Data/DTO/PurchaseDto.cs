using System.ComponentModel.DataAnnotations;

namespace PTLab2_api.Data.DTO
{
    public class PurchaseDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public float UsedPrice { get; set; }

        public PurchaseDto(int id, DateTime date, string address, string productName, float usedPrice)
        {
            Id = id;
            Date = date;
            Address = address;
            ProductName = productName;
            UsedPrice = usedPrice;
        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseDto dto &&
                   Id == dto.Id &&
                   Address == dto.Address &&
                   ProductName == dto.ProductName &&
                   UsedPrice == dto.UsedPrice;
        }
    }
}
