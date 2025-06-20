namespace PruebaFAC.Dto;

public class OrderDto : ResponseBase
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}