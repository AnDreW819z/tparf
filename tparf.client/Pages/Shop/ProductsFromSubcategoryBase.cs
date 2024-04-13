using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Product;

namespace tparf.client.Pages.Shop
{
    public class ProductsFromSubcategoryBase : ComponentBase
    {
        [Parameter]
        public long subId { get; set; }
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
                products = await GetProductCollectionFromSubcategory(subId);

                if (products != null && products.Count() > 0)
                {
                    var productDto = products.FirstOrDefault(p => p.CategoryId == subId);
                    if (productDto != null)
                    {
                        subcategoryName = productDto.CategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private async Task<List<ProductDto>> GetProductCollectionFromSubcategory(long subId)
        {
            var productCollection = await manageProductsLocalStorageService.GetCollection();
            if (productCollection != null)
            {
                return productCollection.Where(p => p.CategoryId == subId).ToList();
            }
            else
            {
                return await productService.GetProductsFromSubcategory(subId);
            }
        }
    }
}
