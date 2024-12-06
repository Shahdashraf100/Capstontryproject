namespace Capstontryproject.Dtos
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
