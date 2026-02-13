namespace Application.Models.Requests
{
    public class Items
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
