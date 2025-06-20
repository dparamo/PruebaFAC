namespace PruebaFAC.Dto;

public class OrderResponseDto : ResponseBase
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }

    public CustomerDto Customer { get; set; } = new();
    public List<OrderItemDto> Items { get; set; } = new();
}
