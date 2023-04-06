using ASMC6P.Shared.Entities;

namespace ASMC6P.Shared.Dtos;

public class ProductDto
{
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}