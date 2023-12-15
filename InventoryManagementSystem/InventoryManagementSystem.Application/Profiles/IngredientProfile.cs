using AutoMapper;
using InventoryManagementSystem.Application.Commands.Ingredients.Create;
using InventoryManagementSystem.Application.Commands.Ingredients.Update;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientSelectListDTO>();
            CreateMap<Ingredient, IngredientListDTO>().ReverseMap();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<CreateIngredientCommand, Ingredient>();
            CreateMap<UpdateIngredientCommand, Ingredient>();
            CreateMap<Price, IngredientPriceDTO>();
        }
    }
}
