namespace ASMC6P.Shared.Dtos
{
    public class OrderDetailsProductDto
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
