namespace InventoryManagementSystem.UI.Services
{
    public partial class ProductSelectListDto
    {
        public override bool Equals(object o)
        {
            var other = o as ProductSelectListDto;
            return other?.Name == Name;
        }
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        public override string ToString() => Name;
    }
}
