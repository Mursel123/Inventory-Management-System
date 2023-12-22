using InventoryManagementSystem.Application.Commands.Orders.Create;
using InventoryManagementSystem.Application.DTOs.Ingredient;
using InventoryManagementSystem.Application.DTOs.Order;
using InventoryManagementSystem.Application.DTOs.OrderLine;
using InventoryManagementSystem.Application.DTOs.Price;
using InventoryManagementSystem.Application.DTOs.Product;
using InventoryManagementSystem.Application.DTOs.Supplier;
using InventoryManagementSystem.Application.Queries.Ingredients.ReadIngredientList;
using InventoryManagementSystem.Application.Queries.Prices.ReadPriceList;
using InventoryManagementSystem.Application.Queries.ReadProductList;
using InventoryManagementSystem.Domain.Entities;
using InventoryManagementSystem.Domain.Enums;
using InventoryManagementSystem.Domain.StaticData;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Charts;

namespace InventoryManagementSystem.Pages.Order
{
    public partial class CreateOrder
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject]
        private IMediator _mediator { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private ICollection<IngredientListDTO> Ingredients { get; set; }
        private ICollection<PriceListDTO> Prices { get; set; }

        private OrderType? selectedOrderType = null;

        public OrderType? SelectedOrderType
        {
            get { return selectedOrderType; }
            set 
            {
                _ = ReadProductsAsync(value);
                selectedOrderType = value;
                DisableOrderType = true;
            }
        }


        private bool DisableOrderType { get; set; } = false;

        private ICollection<ProductListDTO> Products { get; set; }



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

        private ProductListDTO? SelectedProduct { get; set; } = null;

        private IngredientListDTO? selectedIngredient = null;

        public IngredientListDTO? SelectedIngredient
        {
            get { return selectedIngredient; }
            set 
            {
                _ = ReadPriceForIngredientAsync(value);
                selectedIngredient = value;
            }
        }

        private PriceListDTO? SelectedPrice { get; set; } = null;
        private bool ShowLoading { get; set; } = false;
        private OrderDTO Order { get; set; } = new();
        private Func<ProductListDTO, string> productConverter = p => p?.Name;
        private Func<IngredientListDTO, string> ingredientConverter = p => p?.Name;
        private Func<PriceListDTO, string> priceConverter = p => $"Price: {p?.IngredientPrice} Ml: {p?.Ml}";

        private IList<IBrowserFile> files = new List<IBrowserFile>();


        protected override async Task OnInitializedAsync()
        {
            Ingredients = await _mediator.Send(new ReadIngredientListQuery());
        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var command = new CreateOrderCommand(
                    Order.Document,
                    Order.OrderLines,
                    SelectedOrderType);

                await _mediator.Send(command);
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
                    Type = DocumentType.Image
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

        private void Remove(OrderLineDTO? line)
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
                var line = new OrderLineDTO() { Quantity = SelectedQuantity };
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

        private async Task ReadProductsAsync(OrderType? value)
        {
            if (value != null)
            {
                string type = string.Empty;
                if (value == OrderType.Purchased)
                    type = ProductTypeData.PurchasedInventory;
                else if (value == OrderType.Sales)
                    type = ProductTypeData.SalesInventory;


                 Products = await _mediator.Send(new ReadProductListQuery() { ProductType = type });
                if (value == OrderType.Sales)
                {
                    CheckForOutOfStock(SelectedQuantity, Products, value);
                }


            }
        }

        private async Task ReadPriceForIngredientAsync(IngredientListDTO? ingredient)
        {
            if (ingredient != null)
            {
                Prices = await _mediator.Send(new ReadPriceListQuery() { Id = ingredient.Id });
                StateHasChanged();
            }
            
        }

        private void CheckForOutOfStock(int amount, ICollection<ProductListDTO> products, OrderType? type)
        {
            if (type == OrderType.Sales)
            {
                foreach (var product in products)
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

                    if (product.Ingredients.Exists(x => (x.MlUsage - x.MlTotal) < 0) ||
                        product.Ingredients.Exists(x => (x.MlUsage * amount) - x.MlTotal < 0))
                    {
                        IsOutOfStock = true;
                    }

                    product.OutOfStock = IsOutOfStock;
                }
                StateHasChanged();
            }
        }
    }
}
