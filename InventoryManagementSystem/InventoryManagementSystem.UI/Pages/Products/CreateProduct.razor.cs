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
        
        private IBrowserFile file = null;


        protected override async Task OnInitializedAsync()
        {
            ProductTypes = await Client.ReadAllProductTypesAsync();
            Suppliers = await Client.ReadAllSupplierSelectListAsync();
            Ingredients = await Client.ReadAllIngredientSelectListAsync();
            Products = await Client.ReadProductListByTypeAsync(ProductTypeData.PurchasedInventory);
            IsLoading = false;
        }

        private async Task OnSubmitAsync()
        {
            try
            {
                Product.ProductTypes = SelectedProductTypes.ToList();
                Product.Ingredients = SelectedIngredients.Select(x => x.Id).ToList();
                Product.SubProducts = SelectedSubProducts.Select(x => x.Id).ToList();

                if (await FluentValidationValidator!.ValidateAsync())
                {
                    await Client.CreateProductAsync(Product);
                    Snackbar.Add("The product has been successfully added.", Severity.Success);
                }
                    
            }
            catch (Exception)
            {
                Snackbar.Add("Failed to add the product. Please try again later.", Severity.Error);
            }

        }
        private async Task UploadFileAsync(IBrowserFile file)
        {
            if (file.ContentType.StartsWith("image"))
            {
                this.file = file;
                using var stream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(stream);
                Product.Document = new()
                {
                    FileData = stream.ToArray(),
                    Type = DocumentType.Image
                };
            }
            else
            {
                Snackbar.Add("Failed to load the file. Please make sure to select a valid image file.", Severity.Error);
            }


        }

    }
}
