namespace Capstontryproject.Models
{
    public class Product
    {
        public int Id { get; set; } // المعرف الأساسي
        public string Name { get; set; } // اسم المنتج
        public string Description { get; set; } // وصف المنتج
        public decimal Price { get; set; } // سعر المنتج
        public string ImageUrl { get; set; } // رابط صورة المنتج
        public List<CartItem> CartItems { get; set; } // العلاقة مع CartItem
        public List<OrderItem> OrderItems { get; set; } // العلاقة مع OrderItem
    }
}
