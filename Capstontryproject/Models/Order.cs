namespace Capstontryproject.Models
{
    public class Order
    {
        public int Id { get; set; } // المعرف الأساسي
        public int UserId { get; set; } // معرف المستخدم
        public User User { get; set; } // العلاقة مع User (Many-to-One)
        public DateTime OrderDate { get; set; } // تاريخ الطلب
        public decimal TotalAmount { get; set; } // إجمالي المبلغ
        public List<OrderItem> OrderItems { get; set; } // العلاقة مع OrderItem (One-to-Many)
    }
}
