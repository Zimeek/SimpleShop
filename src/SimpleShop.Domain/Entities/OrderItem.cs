namespace SimpleShop.Domain.Entities
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
