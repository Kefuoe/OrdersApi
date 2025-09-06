using System.ComponentModel.DataAnnotations;
using Systme.Collections.Generic;

namespace Orders.Api.Dtos;

public record OrderItemDto(int Quantity, decimal UnitPrice);

public class OrderCreateDto
{
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

