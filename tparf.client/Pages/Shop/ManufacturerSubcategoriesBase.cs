using Microsoft.AspNetCore.Components;
using tparf.client.Interfaces;
using tparf.dto.Product;
using tparf.dto.Subcategories;

namespace tparf.client.Pages.Shop
{
    public class ManufacturerSubcategoriesBase : ComponentBase
    {
        [Parameter]
        public long manId { get; set; }
        [Inject] IProductService productService { get; set; }
        //public List<SubcategoryDto> subcategories { get; set; }

        public List<ProductDto> products { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                //subcategories = await GetSubategoriesCollectionFromManufacturer(manId);
                //products = await productService.GetProducts();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        //private async Task<List<SubcategoryDto>> GetSubategoriesCollectionFromManufacturer(long Id)
        //{
        //    var subCollection = await productService.GetSubcategoriesFromManufacturer(Id);
        //    if (subCollection != null)
        //    {
        //        return subCollection;
        //    }
        //    else
        //    {
        //        return default;
        //    }
        //}
    }
}
