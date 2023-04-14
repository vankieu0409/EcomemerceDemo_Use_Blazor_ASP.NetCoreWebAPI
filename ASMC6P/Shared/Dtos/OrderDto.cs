namespace ASMC6P.Shared.Dtos
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsProductDto> Products { get; set; }
    }
}
