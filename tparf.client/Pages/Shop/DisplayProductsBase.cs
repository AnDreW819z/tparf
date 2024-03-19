using Microsoft.AspNetCore.Components;
using tparf.dto.Product;

namespace tparf.client.Pages.Shop
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public List<ProductDto> products { get; set; }
    }
}
