namespace Data.Models
{
    public class CartProduct
    {
        public long CartId { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
