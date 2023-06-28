using System.ComponentModel.DataAnnotations;

namespace tparf.Models
{
    public class Subcategory

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
