namespace InventoryManagementSystem.Application.DTOs.ProductType
{
    public class ProductTypeDto : BaseDto
    {
        public string Type { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductTypeDto other = (ProductTypeDto)obj;
            return Id == other.Id && Type == other.Type;
        }

        public override int GetHashCode()
        {
            // Use a combination of hash codes of individual properties
            return HashCode.Combine(Id, Type);
        }

        public override string ToString()
        {
            return $"{Id}: {Type}";
        }
    }
}
