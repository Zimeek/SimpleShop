namespace SimpleShop.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
