using EF.Support.Entities.Interfaces;

namespace ASMC6P.Shared.Entities
{
    public class CartItemEntity : IEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
