namespace Application.Models.Requests
{
    public class OrderRequest
    {
        public List<Items> Items { get; set; }
        public Delivery Delivery { get; set; }
        public string? Notes { get; set; }
    }
}
