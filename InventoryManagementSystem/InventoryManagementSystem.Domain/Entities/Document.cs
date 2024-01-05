using InventoryManagementSystem.Domain.Entities.Base;
using InventoryManagementSystem.Domain.Enums;

namespace InventoryManagementSystem.Domain.Entities
{
    public class Document : BaseEntity
    {
        public DocumentType Type { get; set; }
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
        public virtual SubProduct? SubProduct { get; set; }
    }
}