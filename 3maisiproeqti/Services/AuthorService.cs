using _3maisiproeqti.Database.Context;
using _3maisiproeqti.Database.Entity;
using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using System.Data.Entity;

namespace _3maisiproeqti.Services
{
    public class AuthorService : IAuthorRepository
    {
        private readonly ILogger<AuthorService> _logger;
        private readonly BookDBContext _db;
        private readonly IConfiguration _configuration;
        public AuthorService(ILogger<AuthorService> logger, BookDBContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }
        public async Task<CustomResponseModel<AuthorViewModel>> Create(AuthorCreateModel authorCreateModel)
        {
            try
            {
                if (authorCreateModel == null ||
                   authorCreateModel.FirstName.Length < 2 ||
                   authorCreateModel.LastName.Length < 2 
                   )
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Authors.FirstOrDefaultAsync(x => x.FirstName == authorCreateModel.FirstName && x.LastName == authorCreateModel.LastName) != null)
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This author is already registered."
                    };
                }
                var author = new Author()
                {
                    FirstName = authorCreateModel.FirstName,
                    LastName = authorCreateModel.LastName,
                  
                };

                await _db.Authors.AddAsync(author);
                await _db.SaveChangesAsync();

                var authorViewModel = new AuthorViewModel()
                {
                    FirstName = authorCreateModel.FirstName,
                    LastName = authorCreateModel.LastName,
                };
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 200,
                    Result = authorViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<AuthorViewModel>> DeleteById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == Id);
                if (author == null)
                {
                    return new CustomResponseModel<AuthorViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }
                _db.Authors.Remove(author);
                await _db.SaveChangesAsync();

                var authorViewModel = new AuthorViewModel()
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    
                };
                return new CustomResponseModel<AuthorViewModel>
                {
                    StatusCode = 200,
                    Result = authorViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<List<AuthorViewModel>>> GetAll()
        {
            try
            {
                var Authors = await _db.Authors.ToListAsync();
                if (Authors.Count <= 0)
                {
                    return new CustomResponseModel<List<AuthorViewModel>>()
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"

                    };

                }
                var authorView = new List<AuthorViewModel>();
                foreach (var author in Authors)
                {
                    authorView.Add(new AuthorViewModel()
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,

                    });
                }

                return new CustomResponseModel<List<AuthorViewModel>>()
                {
                    StatusCode = 200,
                    Result = authorView,

                };




            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<List<AuthorViewModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<AuthorViewModel>> GetById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == Id);
                if (author == null)
                {
                    return new CustomResponseModel<AuthorViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }

                var authorViewModel = new AuthorViewModel()
                {
                    Id = author.Id,
                    FirstName=author.FirstName,
                    LastName=author.LastName,

                };
                return new CustomResponseModel<AuthorViewModel>
                {
                    StatusCode = 200,
                    Result = authorViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<AuthorViewModel>> Update(AuthorUpdateModel authorUpdateModel)
        {
            try
            {
                if (authorUpdateModel == null ||
                    authorUpdateModel.FirstName.Length < 2 ||
                    authorUpdateModel.LastName.Length < 2
                    )
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Authors.FirstOrDefaultAsync(x => x.FirstName == authorUpdateModel.FirstName && x.LastName == authorUpdateModel.LastName) != null)
                {
                    return new CustomResponseModel<AuthorViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This author is already registered."
                    };
                }
                var author = new Author()
                {
                    FirstName = authorUpdateModel.FirstName,
                    LastName = authorUpdateModel.LastName,

                };

                await _db.Authors.AddAsync(author);
                await _db.SaveChangesAsync();

                var authorViewModel = new AuthorViewModel()
                {
                    FirstName = authorUpdateModel.FirstName,
                    LastName = authorUpdateModel.LastName,
                };
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 200,
                    Result = authorViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<AuthorViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }
    }
}
