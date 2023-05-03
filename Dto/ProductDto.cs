using tparf.Models;

namespace tparf.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
    }
}
