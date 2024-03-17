namespace tparf.api.Entities
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
    }
}
