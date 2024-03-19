using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Product;
using tparf.dto.Subcategories;

namespace tparf.client.Pages.Shop
{
    public class CategoryPageBase : ComponentBase
    {
        [Parameter]
        public long catId { get; set; }
        [Inject] IProductService productService { get; set; }
        [Inject] IManageProductsLocalStorageService manageProductsLocalStorageService { get; set; }
        public List<SubcategoryDto> subcategories { get; set; }

        public List<ProductDto> products { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await manageProductsLocalStorageService.RemoveCollection();
                subcategories = await GetSubategoriesCollectionFromCategory(catId);
                products = await productService.GetProducts();


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<List<SubcategoryDto>> GetSubategoriesCollectionFromCategory(long catId)
        {
            var subCollection = await productService.GetSubcategoriesFromCategory(catId);
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
