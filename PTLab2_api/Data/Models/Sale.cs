using NpgsqlTypes;

namespace PTLab2_api.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public float Value { get; set; }

        public float MinTotalExpenses {  get; set; } 

        public Sale(int id, float value, float minTotalExpenses) 
        {
            Id = id;
            Value = value;
            MinTotalExpenses = minTotalExpenses;
        }

        public override bool Equals(object? obj)
        {
            return obj is Sale sale &&
                   Id == sale.Id &&
                   Value == sale.Value &&
                   MinTotalExpenses == sale.MinTotalExpenses;
        }
    }
}
