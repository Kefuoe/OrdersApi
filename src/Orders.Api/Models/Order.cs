using System.ComponentModel.DataAnnotations;
using Orders.Api.Models;

namespace Orders.Api.Models;

public class Order
{
    public int Id { get; set; }

    public long OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; }
    [MinLength(1)]
    public List<OrderItem> Items { get; set; } = new();
    public int UserId { get; set; }
    public User? User{ get; set; }
}