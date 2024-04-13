using System.ComponentModel.DataAnnotations.Schema;

namespace tparf.api.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Article { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public long ManufacturerId { get; set; }
        public long CategoryId { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Сurrency Currency { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey("СategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<ProductImage>? Images { get; set; }
        public virtual List<Characteristic>? Characteristics { get; set; }
        public virtual List<ProductDescription>? Descriptions { get; set; }
    }
}
