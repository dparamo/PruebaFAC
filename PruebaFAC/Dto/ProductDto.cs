namespace PruebaFAC.Dto
{
    public class ProductDto : ResponseBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }

    }
}