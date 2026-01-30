namespace Domain.Entities
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public Order OrderNavigator { get; set; }
        public long Order { get; set; }
        public Dish DishNavigator { get; set; }
        public Guid Dish { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public Status StatusNavigator { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
