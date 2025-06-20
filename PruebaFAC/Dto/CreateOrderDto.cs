namespace PruebaFAC.Dto
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<CreateItemDto> Items { get; set; } = new();
    }
}
