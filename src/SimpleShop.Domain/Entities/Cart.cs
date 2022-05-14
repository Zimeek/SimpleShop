namespace SimpleShop.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public List<CartItem> Items { get; set; }
        public ApplicationUser User { get; set; }

        public bool IsEmpty => !Items.Any();

        public decimal GetTotal()
        {
            var total = 0m;

            if(Items.Any())
            {
                foreach(var item in Items)
                {
                    total += item.Product.Price * item.Quantity;
                }
            }

            return total;
        }
    }
}
