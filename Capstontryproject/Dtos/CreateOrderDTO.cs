namespace Capstontryproject.Dtos
{
    public class CreateOrderDTO
    {
        public List<int> ProductIds { get; set; }  // قائمة بالمنتجات
        public string ShippingAddress { get; set; }
    }
}
