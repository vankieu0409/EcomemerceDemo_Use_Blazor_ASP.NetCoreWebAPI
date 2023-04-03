using EF.Support.Entities.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace ASMC6P.Shared.Entities
{
    public class ProductEntity : IEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal OriginalPrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal NewPrice { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        [Required]
        public string? Image { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime UploadedDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        //Relationship : many to one
        public CategoryEntity? Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
