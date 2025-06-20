using PruebaFAC.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
