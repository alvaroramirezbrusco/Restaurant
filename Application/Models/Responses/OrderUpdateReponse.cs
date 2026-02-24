namespace Application.Models.Responses
{
    public class OrderUpdateReponse
    {
        public long OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
