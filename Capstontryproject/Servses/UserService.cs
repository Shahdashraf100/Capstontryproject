using Capstontryproject.Dtos;
using Capstontryproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Capstontryproject.Servses
{
    public class UserService
    {
        private readonly dbcontext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(dbcontext context, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> GetCurrentUserAsync()
        {
            // استخراج userId من الـ Claims
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // تحويل userId إلى int
            if (int.TryParse(userIdString, out int userId))
            {
                // إذا كانت التحويلة ناجحة، استرجاع المستخدم من قاعدة البيانات
                var user = await _context.users.FirstOrDefaultAsync(u => u.Id == userId);
                return user;
            }

            return null;  // إذا كانت التحويلة غير ناجحة أو userId فارغ
        }
        public async Task<ServiceResponse<User>> SignUpAsync(UserDTO userDto)
        {
            var response = new ServiceResponse<User>();

            // تحقق من وجود المستخدم مسبقاً
            if (await _context.users.AnyAsync(u => u.Email == userDto.Email))
            {
                response.Success = false;
                response.Message = "Email is already taken.";
                return response;
            }

            // تشفير كلمة المرور
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = user;
            response.Success = true;
            return response;
        }

        // تسجيل دخول
        public async Task<ServiceResponse<User>> LoginAsync(LoginDto loginDto)
        {
            var response = new ServiceResponse<User>();

            var user = await _context.users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";
                return response;
            }

            // تحقق من كلمة المرور باستخدام IPasswordHasher
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";
                return response;
            }

            response.Data = user;
            response.Success = true;
            return response;
        }

    }
}
