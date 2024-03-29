﻿using AutoMapper;
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
            CreateMap<Ingredient, IngredientSelectListDto>();
            CreateMap<Ingredient, IngredientListDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<CreateIngredientCommand, Ingredient>();
            CreateMap<UpdateIngredientCommand, Ingredient>();
            CreateMap<Price, IngredientPriceDTO>();
        }
    }
}
