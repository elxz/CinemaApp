using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cinema.Repositories;
using System.Security.Claims;
using Cinema.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Enum;

namespace Cinema.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<AccountService> logger;
        public AccountService(IUserRepository userRepository, ILogger<AccountService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }
        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var temp_user = await userRepository.GetAll();

                var user = temp_user.FirstOrDefault(x => x.user_login == model.user_login);
                if(user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Login",
                    };
                }

                user = temp_user.FirstOrDefault(x => x.user_phone_number == model.user_phone_number);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Phone",
                    };
                }

                user = new User()
                {
                    user_firstname = model.user_firstname,
                    user_lastname = model.user_lastname,
                    user_login = model.user_login,
                    user_password = HashPasswordHelper.HashPassword(model.user_password),
                    user_phone_number = model.user_phone_number,
                    user_date_of_birth = model.user_date_of_birth,
                    role_ = "Customer",
                };

                await userRepository.Create(user);
                var result = Authentication(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"[Register]: {ex.Message}");

                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var temp_user = await userRepository.GetAll();
                var user = temp_user.FirstOrDefault(x => x.user_login == model.login);

                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if(user.user_password != HashPasswordHelper.HashPassword(model.password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }

                var result = Authentication(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[Login]: {ex.Message}");

                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        private ClaimsIdentity Authentication(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.user_login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role_)
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
