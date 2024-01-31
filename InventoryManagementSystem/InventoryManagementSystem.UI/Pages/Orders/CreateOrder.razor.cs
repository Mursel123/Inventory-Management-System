using Blazored.FluentValidation;
using InventoryManagementSystem.Domain.Enums;
using InventoryManagementSystem.UI.Services;
using InventoryManagementSystem.UI.StaticData;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using DocumentType = InventoryManagementSystem.UI.Services.DocumentType;

namespace InventoryManagementSystem.UI.Pages.Orders
{
    public partial class CreateOrder
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IClient Client { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private List<IngredientListDto> Ingredients { get; set; } = new();
        private List<PriceListDto> Prices { get; set; } = new();

        private Services.OrderType? selectedOrderType = null;

        public Services.OrderType? SelectedOrderType
        {
            get { return selectedOrderType; }
            set
            {
                _ = ReadProductsAsync(value);
                selectedOrderType = value;
                DisableOrderType = true;
            }
        }

        private bool IsLoading { get; set; } = true;
        private bool DisableOrderType { get; set; } = false;

        private List<ProductSelectListDto> Products { get; set; } = new();



        //For OrderLine
        private int selectedQuantity = 1;

        public int SelectedQuantity
        {
            get { return selectedQuantity; }
            set
            {
                CheckForOutOfStock(value, Products, selectedOrderType);
                selectedQuantity = value;
            }
        }

        private ProductSelectListDto? SelectedProduct { get; set; } = null;

        private IngredientListDto? selectedIngredient = null;

        public IngredientListDto? SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                _ = ReadPriceForIngredientAsync(value);
                selectedIngredient = value;
            }
        }

        private PriceListDto? SelectedPrice { get; set; } = null;
        private bool ShowLoading { get; set; } = false;
        private CreateOrderCommand Order { get; set; } = new();
        private Func<ProductSelectListDto, string> productConverter = p => p?.Name;
        private Func<IngredientListDto, string> ingredientConverter = p => p?.Name;
        private Func<PriceListDto, string> priceConverter = p => $"Price: {p?.IngredientPrice} Ml: {p?.Ml}";

        private IList<IBrowserFile> files = new List<IBrowserFile>();
        private FluentValidationValidator FluentValidationValidator = new();

        protected override async Task OnInitializedAsync()
        {
            Order.OrderLines = new();
            Ingredients = await Client.ReadAllIngredientsAsync();
            IsLoading = false;
        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                Order.Type = SelectedOrderType;
                await Client.CreateOrderAsync(Order);

                await JSRuntime.InvokeVoidAsync("history.back");
                Snackbar.Add("The order is added succesfully", Severity.Success);
            }
            catch (Exception)
            {
                Snackbar.Add("The order could not be added", Severity.Error);
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
                Order.Document = new()
                {
                    FileData = stream.ToArray(),
                    Type = DocumentType.Pdf
                };


            }

        }

        private void ShowLoadingForm()
        {
            if (!ShowLoading)
            {
                ShowLoading = true;
            }
        }

        private void Remove(OrderLineDto? line)
        {
            if (line != null)
            {
                Order.OrderLines.Remove(line);

                if (line.Product != null)
                {
                    Products.Add(line.Product);
                }
                else if (line.Ingredient != null)
                {
                    Ingredients.Add(line.Ingredient);
                }


            }
        }
        private void AddItemToList()
        {
            if (SelectedQuantity != 0)
            {
                var line = new OrderLineDto() { Quantity = SelectedQuantity };
                if (SelectedProduct != null)
                {
                    line.Product = SelectedProduct;
                    Products.Remove(line.Product);
                    SelectedProduct = null;
                }
                else if (SelectedIngredient != null && SelectedPrice != null)
                {
                    SelectedIngredient.Prices.Clear();
                    SelectedIngredient.Prices.Add(SelectedPrice);
                    line.Ingredient = SelectedIngredient;
                    Ingredients.Remove(line.Ingredient);
                    SelectedIngredient = null;
                    SelectedPrice = null;
                    Prices.Clear();
                }

                Order.OrderLines.Add(line);
                SelectedQuantity = 0;
                ShowLoading = false;

            }
        }

        private async Task ReadProductsAsync(Services.OrderType? value)
        {
            if (value != null)
            {
                string type = string.Empty;
                if (value == Services.OrderType.Purchased)
                    type = ProductTypeData.PurchasedInventory;
                else if (value == Services.OrderType.Sales)
                    type = ProductTypeData.SalesInventory;


                Products = await Client.ReadProductListByTypeAsync(type);
                if (value == Services.OrderType.Sales)
                {
                    CheckForOutOfStock(SelectedQuantity, Products, value);
                }


            }
        }

        private async Task ReadPriceForIngredientAsync(IngredientListDto? ingredient)
        {
            if (ingredient != null)
            {
                Prices = await Client.ReadPriceListByIngredientIdAsync(ingredient.Id);
                StateHasChanged();
            }

        }

        private void CheckForOutOfStock(int amount, List<ProductSelectListDto> products, Services.OrderType? type)
        {
            if (type == Services.OrderType.Sales)
            {
                /*foreach (var product in products)
                {
                    product.OutOfStock = false;

                    bool IsOutOfStock = false;

                    if (product.Amount <= 0 || (product.Amount - amount) < 0)
                    {
                        IsOutOfStock = true;
                    }

                    if (product.Products.Exists(x => x.Amount <= 0) ||
                        product.Products.Exists(x => (x.Amount - amount) < 0))
                    {
                        IsOutOfStock = true;
                    }

                    if (product.Ingredients.Exists(x => x.MlTotal - (x.MlUsage * amount) < 0))
                    {
                        IsOutOfStock = true;
                    }

                    product.OutOfStock = IsOutOfStock;
                }*/
                StateHasChanged();
            }
        }
    }
}
