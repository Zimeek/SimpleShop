namespace SimpleShop.Domain.Entities
{
    public class OrderDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderId { get; set; }
        public decimal Total { get; set; }
        public Order Order { get; set; }
    }
}
