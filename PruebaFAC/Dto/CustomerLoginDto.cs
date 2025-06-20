namespace PruebaFAC.Dto;

public class CustomerLoginDto : ResponseBase
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
