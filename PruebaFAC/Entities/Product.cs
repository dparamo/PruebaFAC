namespace PruebaFAC.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
