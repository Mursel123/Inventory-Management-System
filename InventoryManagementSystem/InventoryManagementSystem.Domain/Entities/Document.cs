using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Document : BaseEntity
    {
        public DocumentType Type { get; set; }
        public required byte[] FileData { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}