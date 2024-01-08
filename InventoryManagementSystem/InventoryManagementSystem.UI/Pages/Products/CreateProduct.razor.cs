using InventoryManagementSystem.UI.StaticData;
using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Blazored.FluentValidation;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class CreateProduct
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }

        //SelectLists to choose from
        private List<SupplierSelectListDto> Suppliers { get; set; } = new();
        private List<IngredientSelectListDto> Ingredients { get; set; } = new();
        private List<ProductTypeDto> ProductTypes { get; set; } = new();
        private List<ProductSelectListDto> Products { get; set; } = new();
        
        //Selected items
        private IEnumerable<ProductTypeDto> SelectedProductTypes { get; set; } = new HashSet<ProductTypeDto>();
        private IEnumerable<IngredientSelectListDto> SelectedIngredients { get; set; } = new HashSet<IngredientSelectListDto>();
        private IEnumerable<ProductSelectListDto> SelectedSubProducts { get; set; } = new HashSet<ProductSelectListDto>();
        
        
        private bool IsLoading { get; set; } = true;
        private FluentValidationValidator FluentValidationValidator = new();
        private CreateProductCommand Product { get; set; } = new();
        
        private IList<IBrowserFile> files = new List<IBrowserFile>();


        protected override async Task OnInitializedAsync()
        {
            ProductTypes = await Client.ReadAllProductTypesAsync();
            Suppliers = await Client.ReadAllSupplierSelectListAsync();
            Ingredients = await Client.ReadAllIngredientSelectListAsync();
            Products = await Client.ReadAllProductsWithTypeSelectlistAsync(ProductTypeData.PurchasedInventory);
            IsLoading = false;
        }

        private async Task OnSubmit(EditContext context)
        {
            try
            {
                Product.ProductTypes = SelectedProductTypes.ToList();
                Product.Ingredients = SelectedIngredients.Select(x => x.Id).ToList();
                Product.SubProducts = SelectedSubProducts.Select(x => x.Id).ToList();

                if (await FluentValidationValidator!.ValidateAsync())
                {
                    await Client.CreateProductAsync(Product);
                    Snackbar.Add("The product is added succesfully", Severity.Success);
                }
                    
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
            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            Product.Document = new()
            {
                FileData = stream.ToArray(),
                Type = DocumentType._0
            };

        }

    }
}
