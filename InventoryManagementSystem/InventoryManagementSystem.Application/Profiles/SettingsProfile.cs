using AutoMapper;
using InventoryManagementSystem.Application.Commands.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Profiles
{
    internal class SettingsProfile : Profile
    {
        public SettingsProfile()
        {
            CreateMap<CreateSettingsCommand, Domain.Entities.Settings>();
        }
    }
}
