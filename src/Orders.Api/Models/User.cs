namespace Orders.Api.Models;

public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public int Email { get; set; }
    public List<Order> Orders { get; set; } = new();

}