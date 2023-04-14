namespace ASMC6P.Shared.Dtos
{
    public class ProductAdminDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public Guid CategoryId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal NewPrice { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; } = 1;

        public string? Image { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
