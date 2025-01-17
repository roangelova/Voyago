using GetMyTicket.Common.DTOs;
using Microsoft.AspNetCore.Identity;

namespace GetMyTicket.Service.Contracts
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO);
    }
}
