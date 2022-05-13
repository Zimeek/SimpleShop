namespace SimpleShop.Domain.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int Number { get; set; }
        public OrderDetails Details { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ApplicationUser User { get; set; }
    }
}
