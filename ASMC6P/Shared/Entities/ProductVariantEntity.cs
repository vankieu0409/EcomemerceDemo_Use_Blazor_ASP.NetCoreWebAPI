using EF.Support.Entities.Interfaces;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASMC6P.Shared.Entities
{
    public class ProductVariantEntity : IEntity
    {
        [JsonIgnore]
        public ProductEntity? Product { get; set; }
        public Guid ProductId { get; set; }
        public ProductTypeEntity? ProductType { get; set; }
        public Guid ProductTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public bool Visible { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
