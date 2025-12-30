using Femira.api.Data.Entities;
using Femira.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Femira.api.Data.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(DataContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResult> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Mobile_Number == dto.Mobile))
            {
                return ApiResult.Fail("Mobile number already exits");
            }


            var user = new User
            {
                Email = dto.Email,
                Mobile_Number = dto.Mobile,
                full_name = dto.Name
            };

            user.Password_Hash = _passwordHasher.HashPassword(user, dto.Password);

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                //Log the Exception
                // Send Some user friendly error msg to the client

                return ApiResult.Fail(ex.Message);
            }
        }



        public async Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Mobile_Number == dto.Username);
            if(user is null)
            {
                return ApiResult<LoggedInUser>.Fail("User does not exist");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password_Hash, dto.Password);
            if(verificationResult == PasswordVerificationResult.Success)
            {
                return ApiResult<LoggedInUser>.Fail("Incorrect Password");
            }

            // Generate JWT Token
            var jwt = "JWT _TOKEN";

            var loggedInUser = new LoggedInUser(user.Id, user.full_name, user.Mobile_Number, jwt);
            return ApiResult<LoggedInUser> .Success(loggedInUser);
        }
    }


    
}
