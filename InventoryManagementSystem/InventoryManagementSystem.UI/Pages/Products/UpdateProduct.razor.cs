using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using InventoryManagementSystem.UI.Services;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class UpdateProduct
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Parameter]
        public string ProductId { get; set; }
        private ICollection<SupplierSelectListDto> Suppliers { get; set; }
        private ICollection<IngredientSelectListDto> Ingredients { get; set; }
        private ICollection<ProductTypeDto> ProductTypes { get; set; }
        private ProductDto Product { get; set; } = new();

        private IList<IBrowserFile> files = new List<IBrowserFile>();

        private Func<SupplierDto, string> supplierConverter = p => p?.Name;

        private ProductTypeDto? productType;

        public ProductTypeDto? ProductType
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

        private ProductIngredientListDto? ingredient;

        public ProductIngredientListDto? Ingredient
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
           /* Product = await _mediator.Send(new ReadProductByIdQuery(Guid.Parse(ProductId)));


            ProductTypes = await _mediator.Send(new ReadProductTypeListQuery());
            ProductTypes = ProductTypes
                .Where(y => !Product.ProductTypes.Exists(x => x.Id == y.Id))
                .ToList();

            Suppliers = await _mediator.Send(new ReadSupplierListQuery());

            Ingredients = await _mediator.Send(new ReadIngredientListQuery());
            Ingredients = Ingredients
                .Where(y => !Product.Ingredients.Exists(x => x.Id == y.Id))
                .ToList();*/


        }
        private async Task UploadFileAsync(IBrowserFile file)
        {
            if (files.Count >= 1)
            {
                files.Clear();
            }

            files.Add(file);
            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            Product.Document = new()
            {
                FileData = stream.ToArray(),
                Type = DocumentType._0
            };
        }

        private void Remove(object? value)
        {
            if (value != null)
            {
                if (value is ProductTypeDto type)
                {
                    Product.ProductTypes.Remove(type);
                    ProductTypes.Add(type);

                }
                else if (value is ProductIngredientListDto SelectedIngredient)
                {
                    Product.Ingredients.Remove(SelectedIngredient);
                    //Ingredients.Add(SelectedIngredient);

                }

            }
        }
        private void ChangedType(object? value)
        {
            if (value != null)
            {
                if (value is ProductTypeDto type)
                {
                    Product.ProductTypes.Add(type);
                    ProductTypes.Remove(type);
                    ProductType = null;
                }
                else if (value is ProductIngredientListDto SelectedIngredient)
                {
                    Product.Ingredients.Add(SelectedIngredient);
                    //Ingredients.Remove(SelectedIngredient);
                    Ingredient = null;
                }

            }
        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                /*var command = new UpdateProductCommand(
                    Product.Id,
                    Product.Amount,
                    Product.Price,
                    Product.Name,
                    Product.Description,
                    Product.Document,
                    Product.Ingredients,
                    Product.Supplier,
                    Product.ProductTypes);

                await _mediator.Send(command);*/
                Snackbar.Add("The product is updated succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The product could not be updated", Severity.Error);
            }

        }
    }
}
