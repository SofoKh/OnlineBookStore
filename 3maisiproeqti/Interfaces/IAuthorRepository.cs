using _3maisiproeqti.Models;

namespace _3maisiproeqti.Interfaces
{
    public interface IAuthorRepository
    {
        Task<CustomResponseModel<List<AuthorViewModel>>> GetAll();
        Task<CustomResponseModel<AuthorViewModel>> GetById(int Id);
        Task<CustomResponseModel<AuthorViewModel>> Create(AuthorCreateModel authorCreateModel);
        Task<CustomResponseModel<AuthorViewModel>> Update(AuthorUpdateModel authorUpdateModel);
        Task<CustomResponseModel<AuthorViewModel>> DeleteById(int Id);
    }
}
