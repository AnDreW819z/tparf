using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Product;

namespace tparf.client.Pages.Shop
{
    public class DisplayProductsFromSubAndManBase : ComponentBase
    {
        [Parameter]
        public long subId { get; set; }
        [Parameter]
        public long manId { get; set; }
        [Inject] IProductService productService { get; set; }
        [Inject] IManageProductsLocalStorageService manageProductsLocalStorageService { get; set; }

        public List<ProductDto> products { get; set; }
        public string subcategoryName { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await manageProductsLocalStorageService.RemoveCollection();
                products = await GetProductCollectionFromSubcategory(subId, manId);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<List<ProductDto>> GetProductCollectionFromSubcategory(long subId, long manId)
        {
            return await productService.GetProductsFromSubcategoryWithManufacturer(subId, manId);

        }
    }
}

