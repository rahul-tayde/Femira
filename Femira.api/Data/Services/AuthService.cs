using Femira.api.Data.Entities;
using Femira.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Femira.api.Data.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
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
            var jwt = GenerateToken(user);

            var loggedInUser = new LoggedInUser(user.Id, user.full_name, user.Mobile_Number, jwt);
            return ApiResult<LoggedInUser> .Success(loggedInUser);
        }

        private string GenerateToken(User user)
        {
            Claim[] claims = [
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.full_name),
                new (ClaimTypes.MobilePhone, user.Mobile_Number),
                ];

            var secretKey = _configuration.GetValue<string>("Jwt:SecretKey");
            var securityKey = System.Text.Encoding.UTF8.GetBytes(secretKey);
            var symmetricKey = new SymmetricSecurityKey(securityKey);

            var signingCreds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var expireInMinutes = _configuration.GetValue<int>("Jwt:ExpireInMinutes");

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
                signingCredentials: signingCreds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }

    }


    
}
