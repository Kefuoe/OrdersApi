using Microsoft.EntityFrameworkCore;
using Orders.Api.Data;
using Orders.Api.Dtos;
using Orders.Api.Models;

namespace Orders.Api.Services;

public class OrderService : IOrderService
{
    private readonly OrdersDbContext _db;
    private readonly ILogger<OrderService> _logger;

    public OrderService(OrdersDbContext db, Ilogger<OrderService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<OrderReadDto> CreateOrderAsync(OrderCreatedDto dto)
    {
        var order = new Order
        {
            UserId = dto.UserId,
            CreatedUtc = DateTime.UtcNow,
            Status = OrderStatus.Draft,
            Items = dto.Items.Select(i => new OrderItem{Quantity = i.Quantity, UnitPrice = i.UnitPrice}).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Created order {OrderId} with OrderNumber {OrderNumber}", order.Id, order.OrderNumber);

        return ToReadDto(order);
    }

    public async Task<OrderReadDto?> GetByIdAsync(int id)
    {
        var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        return order == null ? null : ToReadDto(order);
    }

    public async Task<List<OrderReadDto>> ListAsync()
    {
        var list = await _db.Orders.Include(o => o.Items).OrderBy(o => o.Id).ToListAsync();
        return list.Select(ToReadDto).ToList();
    }

    public async Task<bool> UpdateAsync(int id, OrderCreatedDto dto)
    {
        var order = await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        if(order == null) return false;

        order.UserId = dto.UserId;
    }
}