namespace SimpleShop.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
