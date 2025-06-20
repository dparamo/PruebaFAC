public class Customer
{
    public Guid Id { get; set; }

    // Autenticación
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // Información personal
    public string Name { get; set; } = string.Empty;
    public required string Email { get; set; }

    // Relación con órdenes
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}