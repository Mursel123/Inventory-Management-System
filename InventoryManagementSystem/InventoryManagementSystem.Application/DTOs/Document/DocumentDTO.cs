using InventoryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.DTOs.Document
{
    public class DocumentDto : BaseDto
    {
        public DocumentType Type { get; set; }
        public byte[] FileData { get; set; }
    }
}
