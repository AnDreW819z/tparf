using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using tparf.client.Interfaces;
using tparf.dto.CartItems;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Images;

namespace tparf.client.Pages.Shop
{
	public class ProductDetailsBase : ComponentBase
	{
		[Parameter]
		public long Id { get; set; }
        [Parameter]
        public List<ProductDto> products { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
		public IProductService ProductService { get; set; }

		[Inject]
		public IShoppingCartService ShoppingCartService { get; set; }

		[Inject]
		public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

		[Inject]
		public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

		[Inject]
		ILocalStorageService _localStorage { get; set; }

		[Inject]
		IProductService _productService { get; set; }

        Color Color = Color.Success;
        public ProductDtos product { get; set; }

		public List<CharacteristicDto> characteristics { get; set; }

        [Inject] IManageProductsLocalStorageService _manageProductsLocalStorageService { get; set; }

        public List<ImageDto> images { get; set; }

        private List<CartItemDto> shoppingCartItems { get; set; }
		public long cartId { get; set; }
		public string errorMessage { get; set; }

		protected override async Task OnInitializedAsync()
        {
			try
			{
               
                cartId = await _localStorage.GetItemAsync<long>("cartId");
                shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
				product = await _productService.GetProduct(Id);
                images = await _productService.GetImagesById(Id);
                products = await GetProductCollectionFromSubcategory(product.CategoryId);
                characteristics = await _productService.GetCharacteristicById(Id);
				
				
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
		{
			try
			{
                //cartId = await _localStorage.GetItemAsync<long>("cartId");

				
                var cartItemDto =  await ShoppingCartService.AddItem(cartItemToAddDto);


                if (cartItemDto != null)
				{

                    shoppingCartItems.Add(cartItemDto);
					if(cartItemDto.Id == 0)
					{
						shoppingCartItems.Remove(cartItemDto);
					}
					//ShoppingCartService.DeleteItem(0);
                    await ManageCartItemsLocalStorageService.SaveCollection(shoppingCartItems);


                }
                navigationManager.NavigateTo("/cart");
               
            }
			catch (Exception ex)
			{
				errorMessage = (ex.Message + " ProductDetailsBase(85)");
				//Log Exception
			}
		}

        private async Task<List<ProductDto>> GetProductCollectionFromSubcategory(long subId)
		{
            var productCollection = await _manageProductsLocalStorageService.GetCollection();
            if (productCollection != null)
            {
                return productCollection.Where(p => p.CategoryId == subId && p.Id != product.Id).ToList();
            }
			else
			{
                return await _productService.GetProductsFromSubcategory(subId);
            }
        }
    }
}
