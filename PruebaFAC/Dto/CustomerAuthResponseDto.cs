namespace PruebaFAC.Dto;

public class CustomerAuthResponseDto : ResponseBase
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}
