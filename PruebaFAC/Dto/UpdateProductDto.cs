namespace PruebaFAC.Dto
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
    }
}
