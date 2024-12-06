namespace Capstontryproject.Models
{
    public class CartItem
    {
        public int Id { get; set; } // المعرف الأساسي
        public int CartId { get; set; } // معرف العربة
        public Cart Cart { get; set; } // العلاقة مع Cart (Many-to-One)
        public int ProductId { get; set; } // معرف المنتج
        public Product Product { get; set; } // العلاقة مع Product (Many-to-One)
        public int Quantity { get; set; } // الكمية
        public decimal Price { get; set; } // السعر
    }
}
