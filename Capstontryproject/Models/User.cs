namespace Capstontryproject.Models
{
    public class User
    {
        public int Id { get; set; } // المعرف الأساسي
        public string Username { get; set; } // اسم المستخدم
        public string Email { get; set; } // البريد الإلكتروني
        public string PasswordHash { get; set; } // كلمة المرور المشفرة
        public DateTime CreatedAt { get; set; } // تاريخ الإنشاء
        public Cart? Cart { get; set; } // العلاقة مع Cart (One-to-One)
        public List<Order>? Orders { get; set; } // العلاقة مع Order (One-to-Many)
    }
}
