using _3maisiproeqti.Models;

namespace _3maisiproeqti.Interfaces
{
    public interface IBookRepository
    {
        Task<CustomResponseModel<List<BookViewModel>>> GetAll();
        Task<CustomResponseModel<BookViewModel>> GetById(int Id);
        Task<CustomResponseModel<BookViewModel>> Create(BookCreateModel bookCreateModel);
        Task<CustomResponseModel<BookViewModel>> Update(BookUpdateModel bookUpdateModel);
        Task<CustomResponseModel<BookViewModel>> DeleteById(int Id);
    }
}
