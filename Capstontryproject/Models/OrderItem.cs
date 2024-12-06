namespace Capstontryproject.Models
{
    public class OrderItem
    {
            public int Id { get; set; } // المعرف الأساسي
            public int OrderId { get; set; } // معرف الطلب
            public Order Order { get; set; } // العلاقة مع Order (Many-to-One)
            public int ProductId { get; set; } // معرف المنتج
            public Product Product { get; set; } // العلاقة مع Product (Many-to-One)
            public int Quantity { get; set; } // الكمية
            public decimal Price { get; set; } // السعر
        

    }
}
