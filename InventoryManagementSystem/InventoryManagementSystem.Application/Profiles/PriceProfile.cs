using AutoMapper;
using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Profiles
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<PriceListDto, Price>().ReverseMap();
        }
    }
}
