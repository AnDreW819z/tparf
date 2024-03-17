using System.ComponentModel.DataAnnotations.Schema;

namespace tparf.api.Entities
{
    public class Subcategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? IconCss { get; set; }
        public string? ImageUrl { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
