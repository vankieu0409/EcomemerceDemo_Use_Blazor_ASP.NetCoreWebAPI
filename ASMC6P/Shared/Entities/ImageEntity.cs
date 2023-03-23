using EF.Support.Entities.Interfaces;

namespace ASMC6P.Shared.Entities
{
    public class ImageEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
