using Cinema.Models.ViewModels;
using Cinema.Response;
using System.Security.Claims;
using Cinema.Models;

namespace Cinema.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
