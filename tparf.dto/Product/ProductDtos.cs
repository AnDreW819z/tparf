using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Descriptions;
using tparf.dto.Product.Images;

namespace tparf.dto.Product
{
    public class ProductDtos
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Article { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public string CurrencySymbol { get; set; }
        public long ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual List<CharacteristicDto>? Characteristics { get; set; }
        public virtual List<ImageDto>? Images { get; set; }
        public virtual List<DescriptionDto>? Descriptions { get; set; }
    }
}
