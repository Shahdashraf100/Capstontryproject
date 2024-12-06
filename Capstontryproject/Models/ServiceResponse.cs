namespace Capstontryproject.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }  // البيانات المطلوبة (النتيجة المرجوة)
        public bool Success { get; set; }  // حالة النجاح (true أو false)
        public string Message { get; set; }  // رسالة توضح حالة العملية (مثل: خطأ، نجاح، إلخ)
    }

}
