using Cinema.Response;
using Cinema.Models;
using Cinema.Service.Interfaces;
using Cinema.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Repositories;
using Cinema.Helpers;

namespace Cinema.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async Task<IBaseResponse<IEnumerable<User>>> IUserService.GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await userRepository.GetAll();
                if (users.Count == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.StatusCode = Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<User>> IUserService.GetUser(int id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await userRepository.Get(id);
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<User>> IUserService.GetUserByLogin(string login)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await userRepository.GetByLogin(login);
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<User>> IUserService.CreateUser(User userModel)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = new User()
                {
                    user_firstname = userModel.user_firstname,
                    user_lastname = userModel.user_lastname,
                    user_login = userModel.user_login,
                    user_password = HashPasswordHelper.HashPassword(userModel.user_password),
                    user_phone_number = userModel.user_phone_number,
                    user_date_of_birth = userModel.user_date_of_birth,
                    role_ = userModel.role_,
                };

                await userRepository.Create(user);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[CreateUser] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        async Task<IBaseResponse<bool>> IUserService.DeleteUser(User user) //int id
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                //var user = await userRepository.Get(id);
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }

                await userRepository.Delete(user);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUser] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> Edit(User model)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await userRepository.Get(model.user_id);
                if (user == null)
                {
                    baseResponse.StatusCode = Enum.StatusCode.UserNotFound;
                    baseResponse.Description = "Пользователь не найден";
                    return baseResponse;
                }

                
                user.user_firstname = model.user_firstname;
                user.user_lastname = model.user_lastname;
                user.user_login = model.user_login;
                user.user_password = model.user_password;
                user.user_phone_number = model.user_phone_number;
                user.user_date_of_birth = model.user_date_of_birth;
                user.role_ = model.role_;

                await userRepository.Update(user);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}