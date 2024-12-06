namespace Capstontryproject.Dtos
{
    public class CartDTO
    {
        public int Id { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
