using Femira.api.Data.Entities;
using Femira.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Femira.api.Data.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;


        public UserService(DataContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;

        }

        public async Task<ApiResult> SaveAddressAsync(AddressDto dto, int userId)
        {
            UserAddress? userAddress = null;
            if (dto.Id == 0)
            {
                var address = new UserAddress
                {
                    Address = dto.Address,
                    Address_Id = dto.Id,
                    IsDefault = dto.IsDefault,
                    Name = dto.name,
                    User_Id = userId
                };
                _context.UserAddresses.Add(userAddress);
            }
            else
            {
                userAddress = await _context.UserAddresses.FindAsync(dto.Id);
                if(userAddress is null)
                {
                    return ApiResult.Fail("Invailid request");
                }

                userAddress.Address = dto.Address;
                userAddress.Name = dto.name;
                userAddress.IsDefault = dto.IsDefault;

                _context.UserAddresses.Update(userAddress);
            }
            try
            {
                if(dto.IsDefault)
                {
                    var defaultAddress = await _context.UserAddresses
                        .FirstOrDefaultAsync(a => a.User_Id == userId && a.IsDefault && a.Address_Id != dto.Id);
                    if (defaultAddress is not null)
                    {
                        defaultAddress.IsDefault = false;
                    }
                }

                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch(Exception ex)
            {
                //log the exception
                return ApiResult.Fail(ex.Message);
            }
        }



        public async Task<AddressDto[]> GetAddresses(int userId) =>
            await _context.UserAddresses
            .AsNoTracking()
            .Where(a => a.User_Id == userId)
            .Select(a => new AddressDto
            {
                Id = a.User_Id,
                Address = a.Address,
                IsDefault = a.IsDefault,
                name = a.Name,
            })
            .ToArrayAsync();

        public async Task<ApiResult> ChangePasswordAsync(ChangePasswordDto dto, int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user is null)
                    return ApiResult.Fail("User Does Not Exist");

                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password_Hash, dto.CurrentPassword);
                if (verificationResult != PasswordVerificationResult.Success)
                    return ApiResult.Fail("Incorrect Password");

                user.Password_Hash = _passwordHasher.HashPassword(user, dto.NewPassword);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return ApiResult.Success();

            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }



    }


    
}
