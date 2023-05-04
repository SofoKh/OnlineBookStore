using _3maisiproeqti.Database.Context;
using _3maisiproeqti.Database.Entity;
using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using System.Data.Entity;

namespace _3maisiproeqti.Services
{
    public class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly BookDBContext _db;
        private readonly IConfiguration _configuration;
        public UserService(ILogger<UserService> logger, BookDBContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }
        public async Task<CustomResponseModel<UserViewModel>> Create(UserViewModel userCreateModel)
        {
            try
            {
                if (userCreateModel == null ||
                   userCreateModel.Username.Length < 2 ||
                   userCreateModel.Password.Length < 2
                   )
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Users.FirstOrDefaultAsync(x => x.Username == userCreateModel.Username) != null)
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This User is already registered."
                    };
                }
                var Users = await _db.Users.ToListAsync();
                if (Users.Count <= 0)
                {
                    var user = new User()
                    {
                        Username = userCreateModel.Username,
                        Password = userCreateModel.Password,
                        Role = 1,

                    };

                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();


                }
                else
                {
                    var user = new User()
                    {
                        Username = userCreateModel.Username,
                        Password = userCreateModel.Password,
                        Role = 0

                    };

                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();

                }

                var userViewModel = new UserViewModel()
                {
                    Username = userCreateModel.Username,
                    Password = userCreateModel.Password,
                };
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 200,
                    Result = userViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<UserViewModel>> DeleteById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == Id);
                if (user == null)
                {
                    return new CustomResponseModel<UserViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,

                };
                return new CustomResponseModel<UserViewModel>
                {
                    StatusCode = 200,
                    Result = userViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<List<UserViewModel>>> GetAll()
        {
            try
            {
                var Users = await _db.Users.ToListAsync();
                if (Users.Count <= 0)
                {
                    return new CustomResponseModel<List<UserViewModel>>()
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"

                    };

                }
                var userView = new List<UserViewModel>();
                foreach (var user in Users)
                {
                    userView.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password,

                    });
                }

                return new CustomResponseModel<List<UserViewModel>>()
                {
                    StatusCode = 200,
                    Result = userView,

                };




            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<List<UserViewModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<UserViewModel>> GetById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == Id);
                if (user == null)
                {
                    return new CustomResponseModel<UserViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }

                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,

                };
                return new CustomResponseModel<UserViewModel>
                {
                    StatusCode = 200,
                    Result = userViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<UserViewModel>> Update(UserViewModel userUpdateModel)
        {
            try
            {
                if (userUpdateModel == null ||
                    userUpdateModel.Username.Length < 2 ||
                    userUpdateModel.Password.Length < 2
                    )
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Users.FirstOrDefaultAsync(x => x.Username == userUpdateModel.Username ) != null)
                {
                    return new CustomResponseModel<UserViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This user is already registered."
                    };
                }
                var user = new User()
                {
                    Username = userUpdateModel.Username,
                    Password = userUpdateModel.Password,

                };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                var userViewModel = new UserViewModel()
                {
                    Username = userUpdateModel.Username,
                    Password = userUpdateModel.Password,
                };
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 200,
                    Result = userViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<UserViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }



    }
}

