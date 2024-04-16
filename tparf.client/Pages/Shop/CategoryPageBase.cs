using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Categories;
using tparf.dto.Product;

namespace tparf.client.Pages.Shop
{
    public class CategoryPageBase : ComponentBase
    {
        [Parameter]
        public long catId { get; set; }
        [Inject] IProductService productService { get; set; }
        [Inject] IManageProductsLocalStorageService manageProductsLocalStorageService { get; set; }
        
        public List<ProductDto> products { get; set; }
        public CategoryDto category { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await manageProductsLocalStorageService.RemoveCollection();
                category = await GetCategory(catId);
                products = await productService.GetProductsFromCategory(catId);


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<CategoryDto> GetCategory(long catId)
        {
            var subCollection = await productService.GetCategory(catId);
            if (subCollection != null)
            {
                return subCollection;
            }
            else
            {
                return default;
            }
        }
    }
}
