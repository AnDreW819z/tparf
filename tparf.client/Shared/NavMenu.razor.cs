using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using tparf.client.Interfaces;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;
using tparf.dto.Subcategories;

namespace tparf.client.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        public IProductService productService { get; set; }
        public List<ManufacturerDto> manufacturerDtos { get; set; }
        public List<CategoryDto> categoryDtos { get; set; }
        public List<SubcategoryDto> subcategoryDtos { get; set; }
		public List<SubcategoryDto> subcategoryManDtos { get; set; }
		public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                
                categoryDtos = await productService.GetCategories();
                subcategoryDtos= await productService.GetSubcategories();
				manufacturerDtos = await productService.GetManufacturers();

			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

  //      private async Task<List<SubcategoryDto>> GetSubByMan(long manId)
  //      {
  //          try
  //          {
  //              var result = await productService.GetSubcategoriesFromManufacturer(manId);
  //              if (result != null)
  //              {
  //                  return result;
  //              }
  //              return default(List<SubcategoryDto>);
  //          }
		//	catch (Exception ex)
		//	{
		//		errorMessage = ex.Message;
  //              throw;
		//	}
		//}
    }
}
