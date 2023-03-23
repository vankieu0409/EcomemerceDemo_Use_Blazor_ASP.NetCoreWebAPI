using EF.Support.Entities.Interfaces;

using System.ComponentModel.DataAnnotations.Schema;

namespace ASMC6P.Shared.Entities
{
    public class ProductTypeEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
