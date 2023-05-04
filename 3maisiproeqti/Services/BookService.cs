using _3maisiproeqti.Database.Context;
using _3maisiproeqti.Database.Entity;
using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using System.Data.Entity;

namespace _3maisiproeqti.Services
{
    public class BookService : IBookRepository
    {
        private readonly ILogger<BookService> _logger;
        private readonly BookDBContext _db;
        private readonly IConfiguration _configuration;
        public BookService(ILogger<BookService> logger, BookDBContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }
        public async Task<CustomResponseModel<BookViewModel>> Create(BookCreateModel bookCreateModel)
        {
            try
            {
                if (bookCreateModel == null ||
                   bookCreateModel.Genre.Length < 2 ||
                   bookCreateModel.ISBN.Length < 2 ||
                   bookCreateModel.Title.Length < 2 ||
                   bookCreateModel.PublicationYear < -2285 ||
                   bookCreateModel.AuthorId < 0 ||
                   bookCreateModel.Price < 0
                   )
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Books.FirstOrDefaultAsync(x => x.Title == bookCreateModel.Title) != null)
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This book is already registered."
                    };
                }
                var book = new Book()
                {
                    Title = bookCreateModel.Title,
                    Genre = bookCreateModel.Genre,
                    Price = bookCreateModel.Price,
                    AuthorId = bookCreateModel.AuthorId,
                    PublicationYear = bookCreateModel.PublicationYear,
                    ISBN = bookCreateModel.ISBN

                };

                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();

                var bookViewModel = new BookViewModel()
                {
                    Title = bookCreateModel.Title,
                    Genre = bookCreateModel.Genre,
                    Price = bookCreateModel.Price,
                    AuthorId = bookCreateModel.AuthorId,
                    PublicationYear = bookCreateModel.PublicationYear,
                    ISBN = bookCreateModel.ISBN
                };
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 200,

                    Result = bookViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<BookViewModel>> DeleteById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == Id);
                if (book == null)
                {
                    return new CustomResponseModel<BookViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                var bookViewModel = new BookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Price = book.Price,
                    AuthorId = book.AuthorId,
                    PublicationYear = book.PublicationYear,
                    ISBN = book.ISBN

                };
                return new CustomResponseModel<BookViewModel>
                {
                    StatusCode = 200,
                    Result = bookViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<List<BookViewModel>>> GetAll()
        {
            try
            {
                var Books = await _db.Books.ToListAsync();
                if (Books.Count <= 0)
                {
                    return new CustomResponseModel<List<BookViewModel>>()
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"

                    };

                }
                var bookView = new List<BookViewModel>();
                foreach (var book in Books)
                {
                    bookView.Add(new BookViewModel()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Genre = book.Genre,
                        Price = book.Price,
                        AuthorId = book.AuthorId,
                        PublicationYear = book.PublicationYear,
                        ISBN = book.ISBN

                    });
                }

                return new CustomResponseModel<List<BookViewModel>>()
                {
                    StatusCode = 200,
                    Result = bookView,

                };




            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<List<BookViewModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<BookViewModel>> GetById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "Invalid Id"
                    };
                }
                var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == Id);
                if (book == null)
                {
                    return new CustomResponseModel<BookViewModel>
                    {
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                    };
                }

                var bookViewModel = new BookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Price = book.Price,
                    AuthorId = book.AuthorId,
                    PublicationYear = book.PublicationYear,
                    ISBN = book.ISBN

                };
                return new CustomResponseModel<BookViewModel>
                {
                    StatusCode = 200,
                    Result = bookViewModel,
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }

        public async Task<CustomResponseModel<BookViewModel>> Update(BookUpdateModel bookUpdateModel)
        {
            try
            {
                if (bookUpdateModel == null ||
                   bookUpdateModel.Genre.Length < 2 ||
                   bookUpdateModel.ISBN.Length < 2 ||
                   bookUpdateModel.Title.Length < 2 ||
                   bookUpdateModel.PublicationYear < -2285 ||
                   bookUpdateModel.AuthorId < 0 ||
                   bookUpdateModel.Price < 0
                   )
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "invalid Input"
                    };
                };
                if (await _db.Books.FirstOrDefaultAsync(x => x.Title == bookUpdateModel.Title) != null)
                {
                    return new CustomResponseModel<BookViewModel>()
                    {
                        StatusCode = 422,
                        ErrorMessage = "This book is already registered."
                    };
                }
                var book = new Book()
                {
                    Title = bookUpdateModel.Title,
                    Genre = bookUpdateModel.Genre,
                    Price = bookUpdateModel.Price,
                    AuthorId = bookUpdateModel.AuthorId,
                    PublicationYear = bookUpdateModel.PublicationYear,
                    ISBN = bookUpdateModel.ISBN

                };

                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();

                var bookViewModel = new BookViewModel()
                {
                    Title = bookUpdateModel.Title,
                    Genre = bookUpdateModel.Genre,
                    Price = bookUpdateModel.Price,
                    AuthorId = bookUpdateModel.AuthorId,
                    PublicationYear = bookUpdateModel.PublicationYear,
                    ISBN = bookUpdateModel.ISBN
                };
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 200,

                    Result = bookViewModel
                };

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new CustomResponseModel<BookViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = "Something went wrong"
                };
            }
        }
    }
}
