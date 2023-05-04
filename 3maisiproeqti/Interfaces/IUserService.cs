using _3maisiproeqti.Models;

namespace _3maisiproeqti.Interfaces
{
    public interface IUserService
    {
        Task<CustomResponseModel<List<UserViewModel>>> GetAll();
        Task<CustomResponseModel<UserViewModel>> GetById(int Id);
        Task<CustomResponseModel<UserViewModel>> Create(UserCreateModel userCreateModel);
        Task<CustomResponseModel<UserViewModel>> Update(UserUpdateModel userUpdateModel);
        Task<CustomResponseModel<UserViewModel>> DeleteById(int Id);
   
    }
}
