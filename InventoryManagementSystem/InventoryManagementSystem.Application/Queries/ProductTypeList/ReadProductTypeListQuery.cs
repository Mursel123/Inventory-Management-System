﻿using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.ProductTypeList
{
    public class ReadProductTypeListQuery : IRequest<List<ProductTypeDTO>>
    {
    }
}