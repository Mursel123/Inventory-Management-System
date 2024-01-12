using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using InventoryManagementSystem.UI.Services;
using Blazored.FluentValidation;
using InventoryManagementSystem.UI.StaticData;


namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class UpdateProduct
    {
        [Parameter]
        public string ProductId { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }
        public UpdateProductCommand Product { get; set; } = new();

        //SelectLists to choose from
        private List<SupplierSelectListDto> Suppliers { get; set; } = new();
        private List<IngredientSelectListDto> Ingredients { get; set; } = new();
        private List<ProductTypeDto> ProductTypes { get; set; } = new();
        private List<ProductSelectListDto> Products { get; set; } = new();

        //Selected items
        public SupplierSelectListDto SelectedSupplier { get; set; } = new();
        private IEnumerable<ProductTypeDto> SelectedProductTypes { get; set; } = new HashSet<ProductTypeDto>();
        private IEnumerable<IngredientSelectListDto> SelectedIngredients { get; set; } = new HashSet<IngredientSelectListDto>();
        private IEnumerable<ProductSelectListDto> SelectedSubProducts { get; set; } = new HashSet<ProductSelectListDto>();


        private bool IsLoading { get; set; } = true;
        private FluentValidationValidator FluentValidationValidator = new();

        private IBrowserFile file = null;


        protected override async Task OnInitializedAsync()
        {
            var product = await Client.ReadProductByIdAsync(Guid.Parse(ProductId));
            Product = new UpdateProductCommand()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                Price = product.Price,
            };


            if (product.Supplier != null)
            {
                SelectedSupplier = new SupplierSelectListDto()
                {
                    Id = product.Supplier.Id,
                    Name = product.Supplier.Name,
                };
            }
            

            SelectedProductTypes = product.ProductTypes.ToList();

            SelectedIngredients = product.Ingredients.Select(x => new IngredientSelectListDto
            {
                Id = x.Id,
                MlUsage = x.MlUsage,
                Name = x.Name

            }).ToList();


            SelectedSubProducts = product.SubProducts.Select(x => new ProductSelectListDto
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();

            
            ProductTypes = await Client.ReadAllProductTypesAsync();
            Suppliers = await Client.ReadAllSupplierSelectListAsync();
            Ingredients = await Client.ReadAllIngredientSelectListAsync();
            Products = await Client.ReadAllProductsWithTypeSelectlistAsync(ProductTypeData.PurchasedInventory);

            IsLoading = false;
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
                    Type = DocumentType._0
                };
            }
            else
            {
                Snackbar.Add("Failed to load the file. Please make sure to select a valid image file.", Severity.Error);
            }

        }




        private async Task OnSubmitAsync()
        {
            try
            {
                Product.SupplierId = SelectedSupplier.Id;
                Product.Ingredients = SelectedIngredients.Select(x => x.Id).ToList();
                Product.SubProducts = SelectedSubProducts.Select(x => x.Id).ToList();
                Product.ProductTypes = SelectedProductTypes.ToList();

                if (await FluentValidationValidator!.ValidateAsync())
                {
                    await Client.UpdateProductAsync(Product);
                    Snackbar.Add("The product has been successfully updated.", Severity.Success);
                }

            }
            catch (Exception)
            {
                Snackbar.Add("Failed to update the product. Please try again later.", Severity.Error);
            }

        }
    }
}
