using InventoryManagementSystem.Application.Commands.Products.CreateProduct;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.DTOs.ProductType;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList;
using InventoryManagementSystem.Application.Queries.ProductTypeList;
using InventoryManagementSystem.Application.Queries.Suppliers.ReadSupplierSelectList;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InventoryManagementSystem.Pages.Product
{
    public partial class CreateProduct
    {
        [Inject] 
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }
        private ICollection<SupplierDTO> Suppliers { get; set; }
        private ICollection<IngredientListDTO> Ingredients { get; set; }
        private ICollection<ProductTypeDTO> ProductTypes { get; set; }
       
        private ProductDTO Product { get; set; } = new();

        private IList<IBrowserFile> files = new List<IBrowserFile>();


        private ProductTypeDTO? productType;

        public ProductTypeDTO? ProductType
        {
            get { return productType; }
            set 
            {
                if (value != null)
                {
                    ChangedType(value);
                }
                else
                {
                    productType = value;
                }
                
            }
        }

        private IngredientListDTO? ingredient;

        public IngredientListDTO? Ingredient
        {
            get { return ingredient; }
            set
            {
                if (value != null)
                {
                    ChangedType(value);
                }
                else
                {
                    ingredient = value;
                }

            }
        }

        protected override async Task OnInitializedAsync()
        {
            ProductTypes = await _mediator.Send(new ReadProductTypeListQuery());
            Suppliers = await _mediator.Send(new ReadSupplierListQuery());
            Ingredients = await _mediator.Send(new ReadIngredientListQuery());
            

        }
        
        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var command = new CreateProductCommand(
                    Product.Amount,
                    Product.Price,
                    Product.Name,
                    Product.Description,
                    Product.Document,
                    Product.Ingredients,
                    Product.Supplier,
                    Product.ProductTypes);

                await _mediator.Send(command);

                Snackbar.Add("The product is added succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The product could not be added", Severity.Error);
            }
            
        }
        private async Task UploadFileAsync(IBrowserFile file)
        {
            if (files.Count >= 1)
            {
                files.Clear();
            }

            files.Add(file);
            using (var stream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(stream);
                Product.Document = new()
                {
                    FileData = stream.ToArray(),
                    Type = DocumentType.Image
                };


            }

        }

        private void Remove(object? value)
        {
            if (value != null)
            {
                if (value is ProductTypeDTO type)
                {
                    Product.ProductTypes.Remove(type);
                    ProductTypes.Add(type);

                }
                else if (value is IngredientListDTO SelectedIngredient)
                {
                    Product.Ingredients.Remove(SelectedIngredient);
                    Ingredients.Add(SelectedIngredient);

                }

            }
        }
        private void ChangedType(object? value)
        {
            if (value != null)
            {
                if (value is ProductTypeDTO type)
                {
                    Product.ProductTypes.Add(type);
                    ProductTypes.Remove(type);
                    ProductType = null;
                }
                else if (value is IngredientListDTO SelectedIngredient)
                {
                    Product.Ingredients.Add(SelectedIngredient);
                    Ingredients.Remove(SelectedIngredient);
                    Ingredient = null;
                }

            }
        }


    }
}
