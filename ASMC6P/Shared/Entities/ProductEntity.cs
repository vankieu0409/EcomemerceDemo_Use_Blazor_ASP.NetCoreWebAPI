using EF.Support.Entities.Interfaces;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASMC6P.Shared.Entities
{
    public class ProductEntity : IEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        public CategoryEntity? Category { get; set; }
        public Guid CategoryId { get; set; }
        public bool Featured { get; set; } = false;
        public List<ProductVariantEntity> Variants { get; set; } = new List<ProductVariantEntity>();
        public bool Visible { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
