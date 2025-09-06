using Orders.Api.Models;

namespace Orders.Api.Dtos;

public class OrderReadDto
{
    public int Id { get; set; }
    public long OrderNumber { get; set; }
    public DateTime CreatedUtc { get; set; }
    public int UserId { get; set; }
    public int status { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}