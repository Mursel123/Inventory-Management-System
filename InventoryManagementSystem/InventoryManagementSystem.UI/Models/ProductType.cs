
namespace InventoryManagementSystem.UI.Services
{
    public partial class ProductTypeDto
    {
        public override bool Equals(object o)
        {
            var other = o as ProductTypeDto;
            return other?.Type == Type;
        }
        public override int GetHashCode() => Type?.GetHashCode() ?? 0;

        public override string ToString() => Type;
    }
}
