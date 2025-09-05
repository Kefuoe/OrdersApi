using System.ComponentModel.DataAnnotations;

namespace Orders.Api.Models;

public class OrderItem
{
    public int Id { get; set; }
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }
    public int OrderId { get; set; }
    public Order? Order{ get; set; }

}