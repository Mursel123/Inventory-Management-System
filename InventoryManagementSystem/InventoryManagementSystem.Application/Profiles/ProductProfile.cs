using AutoMapper;
using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.Commands.Products.UpdateProduct;
using InventoryManagementSystem.Application.DTOs.Document;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductType, ProductTypeDTO>();
            CreateMap<Ingredient, ProductIngredientDTO>();
            CreateMap<Price, IngredientPriceDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<OrderLine, ProductOrderLineDTO>();
            CreateMap<Product, Product>();
            CreateMap<ProductProductDTO, Product>();
            CreateMap<Product, ProductProductDTO>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>()
                .ForPath(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.ProductTypes, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<ProductDTO, UpdateProductCommand>();
        }
    }
}
