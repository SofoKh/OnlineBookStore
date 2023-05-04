using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3maisiproeqti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userRepository;
        public UserController(IUserService userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<List<UserViewModel>>> GetAll() => await _userRepository.GetAll();
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<UserViewModel>> GetById(int Id) => await _userRepository.GetById(Id);
        [HttpPost("[action]")]
        public async Task<CustomResponseModel<UserViewModel>> Create([FromQuery] UserCreateModel userCreateModel) => await _userRepository.Create(userCreateModel);
        [HttpPut("[action]")]
        public async Task<CustomResponseModel<UserViewModel>> Update([FromQuery] UserUpdateModel userUpdateModel) => await _userRepository.Update(userUpdateModel);
        [HttpDelete("[action]")]
        public async Task<CustomResponseModel<UserViewModel>> DeleteById(int Id) => await _userRepository.DeleteById(Id);
    }
}
