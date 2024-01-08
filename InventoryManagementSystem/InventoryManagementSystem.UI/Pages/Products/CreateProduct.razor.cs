using InventoryManagementSystem.UI.StaticData;
using InventoryManagementSystem.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InventoryManagementSystem.UI.Pages.Products
{
    public partial class CreateProduct
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }
        private List<SupplierSelectListDto> Suppliers { get; set; } = new();
        private List<IngredientSelectListDto> Ingredients { get; set; } = new();
        private List<ProductTypeDto> ProductTypes { get; set; } = new();
        private List<ProductSelectListDto> Products { get; set; } = new();
        private bool IsLoading { get; set; } = true;

        private CreateProductCommand Product { get; set; } = new();
        
        private IList<IBrowserFile> files = new List<IBrowserFile>();

        private ProductTypeDto? selectedProductType = null;

        public ProductTypeDto? SelectedProductType
        {
            get { return selectedProductType; }
            set
            {
                if (value != null)
                {
                    ChangedType(value);
                }
                else
                {
                    selectedProductType = value;
                }

            }
        }

        private IngredientSelectListDto? selectedIngredient = null;

        public IngredientSelectListDto? SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                if (value != null)
                {
                    ChangedType(value);
                }
                else
                {
                    selectedIngredient = value;
                }

            }
        }


        private ProductSelectListDto? selectedProduct = null;

        public ProductSelectListDto? SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (value != null)
                {
                    ChangedType(value);
                }
                else
                {
                    selectedProduct = value;
                }

            }
        }
        protected override async Task OnInitializedAsync()
        {
            Product.Ingredients = new();
            Product.ProductTypes = new();
            Product.SubProducts = new();

            ProductTypes = await Client.ReadAllProductTypesAsync();
            Suppliers = await Client.ReadAllSupplierSelectListAsync();
            Ingredients = await Client.ReadAllIngredientSelectListAsync();
            Products = await Client.ReadAllProductsWithTypeSelectlistAsync(ProductTypeData.PurchasedInventory);
            IsLoading = false;

        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                await Client.CreateProductAsync(Product);
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
                    Type = Services.DocumentType._0
                };


            }

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
                else if (value is IngredientSelectListDto SelectedIngredient)
                {
                    Product.Ingredients.Remove(SelectedIngredient);
                    Ingredients.Add(SelectedIngredient);

                }
                else if (value is ProductSelectListDto selectedProduct)
                {
                    Product.SubProducts.Remove(selectedProduct);
                    Products.Add(selectedProduct);

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
                    SelectedProductType = null;
                }
                else if (value is IngredientSelectListDto SelectedIngredient)
                {
                    Product.Ingredients.Add(SelectedIngredient);
                    Ingredients.Remove(SelectedIngredient);
                    this.SelectedIngredient = null;
                }
                else if (value is ProductSelectListDto selectedProduct)
                {
                    Product.SubProducts.Add(selectedProduct);
                    Products.Remove(selectedProduct);
                    SelectedProduct = null;
                }

            }
        }
    }
}
