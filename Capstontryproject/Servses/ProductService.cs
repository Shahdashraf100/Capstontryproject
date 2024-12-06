using Capstontryproject.Dtos;
using Capstontryproject.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstontryproject.Servses
{
   public class ProductService
    {
        private readonly dbcontext _context;

        public ProductService(dbcontext context)
        {
            _context = context;
        }

        // الحصول على كل المنتجات
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return await _context.products
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();
        }

        // الحصول على منتج معين بواسطة الـ ID
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.products
                .Where(p => p.Id == id)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .FirstOrDefaultAsync();

            return product;
        }


     
            public async Task<ServiceResponse<Product>> AddProductAsync(CreateProductDTO productDto)
            {
                var response = new ServiceResponse<Product>();

                // تحويل الـ DTO إلى نموذج الـ Product
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    ImageUrl = productDto.ImageUrl,
                 
                };

                // إضافة المنتج إلى قاعدة البيانات
                await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();

                response.Data = product;
                response.Success = true;
                response.Message = "Product created successfully.";

                return response;
            }
        }






    

}
