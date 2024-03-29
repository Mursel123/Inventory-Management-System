﻿using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.ProductType;

namespace InventoryManagementSystem.Application.DTOs.Product
{
    public class ProductListDto : BaseDto
    {
        public int Amount { get; set; }
        public decimal Price { get; set; } 
        public string Name { get; set; } = string.Empty;
        public virtual List<ProductTypeDto> ProductTypes { get; set; } = new();
        public string ProductTypesString 
        { 
            get 
            { 
                return string.Join(", ", ProductTypes.Select(pt => pt.Type)); 
            } 
        }


    }
}
