using System.ComponentModel.DataAnnotations.Schema;

namespace tparf.api.Entities
{
    public class ProductDescription
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product {get; set;}
    }
}
