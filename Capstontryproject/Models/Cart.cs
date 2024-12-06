namespace Capstontryproject.Models
{
    public class Cart
    {
       
            public int Id { get; set; } // المعرف الأساسي
            public int UserId { get; set; } // معرف المستخدم
            public User User { get; set; } // العلاقة مع User (One-to-One)
            public List<CartItem> CartItems { get; set; } // العلاقة مع CartItem (One-to-Many)
            public DateTime CreatedAt { get; set; } // تاريخ إنشاء العربة
            public decimal TotalPrice
            {
                get => CartItems?.Sum(ci => ci.Price * ci.Quantity) ?? 0; // إجمالي السعر
            }
        

    }
}
