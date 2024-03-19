using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Categories;
using tparf.dto.Subcategories;

namespace tparf.client.Pages.Shop
{
    public class GeneralBase : ComponentBase
    {
        [Inject] IProductService productService { get; set; }
        [Inject] IManageProductsLocalStorageService manageProductsLocalStorageService { get; set; }
        public List<CategoryDto> categories { get; set; }

        public List<SubcategoryDto> subcategories { get; set; }
        public string manufacturerName { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await manageProductsLocalStorageService.RemoveCollection();
                categories = await GetCategoriesCollection();
                subcategories = await productService.GetSubcategories();

                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<List<CategoryDto>> GetCategoriesCollection()
        {
            var catCollection = await productService.GetCategories();
            if (catCollection != null)
            {
                return catCollection;
            }
            else
            {
                return default;
            }
        }
    }
}
