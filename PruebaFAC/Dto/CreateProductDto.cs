namespace PruebaFAC.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
