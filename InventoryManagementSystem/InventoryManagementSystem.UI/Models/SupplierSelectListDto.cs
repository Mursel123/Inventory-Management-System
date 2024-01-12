namespace InventoryManagementSystem.UI.Services
{
    public partial class SupplierSelectListDto
    {
        public override bool Equals(object o)
        {
            var other = o as SupplierSelectListDto;
            return other?.Name == Name;
        }
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        public override string ToString() => Name;
    }
}
