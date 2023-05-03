namespace tparf.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductProperty> Properties { get; set; }
    }
}
