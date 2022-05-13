namespace SimpleShop.Domain.Entities
{
    public class CartItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
