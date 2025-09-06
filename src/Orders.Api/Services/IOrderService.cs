using Orders.Api.Dtos;

namespace Orders.Api.Services;

public interface IOrderService
{
    Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto);
    Task<OrderReadDto> GetByIdAsync(int id);
    Task<List<OrderReadDto>> ListAsync();
    Task<bool> UpdateAsync(int id, OrderCreatedDto dto);
    Task<bool> DeleteAsync(int id);
}