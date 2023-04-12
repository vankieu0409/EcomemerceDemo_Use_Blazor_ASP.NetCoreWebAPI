using ASMC6P.Shared.Dtos;

namespace ASMC6P.Shared.ViewModels;

public class CreateOrderViewModel
{
    public Guid UserId { get; set; }
    public List<CartProductDto> ProductCollection { get; set; }
}