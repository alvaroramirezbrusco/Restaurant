namespace Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}
