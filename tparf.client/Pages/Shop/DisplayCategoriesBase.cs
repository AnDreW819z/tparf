using Microsoft.AspNetCore.Components;
using tparf.dto.Categories;

namespace tparf.client.Pages.Shop
{
    public class DisplayCategoriesBase : ComponentBase
    {
        [Parameter]
        public List<CategoryDto> categories { get; set; }
        
    }
}
