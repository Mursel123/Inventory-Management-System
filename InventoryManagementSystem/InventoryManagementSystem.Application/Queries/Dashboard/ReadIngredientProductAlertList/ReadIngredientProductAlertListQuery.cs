using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Queries.Dashboard.ReadIngredientProductAlertList
{
    public class ReadIngredientProductAlertListQuery : IRequest<(List<ProductListDto>, List<IngredientListDTO>)>
    {
    }
}
