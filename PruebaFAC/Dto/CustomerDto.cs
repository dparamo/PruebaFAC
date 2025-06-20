using PruebaFAC.Dto;
using System.ComponentModel.DataAnnotations;

public class CustomerDto : ResponseBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
