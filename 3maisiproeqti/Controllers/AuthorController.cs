using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3maisiproeqti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<List<AuthorViewModel>>> GetAll() => await _authorRepository.GetAll();
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<AuthorViewModel>> GetById(int Id) => await _authorRepository.GetById(Id);
        [HttpPost("[action]")]
        public async Task<CustomResponseModel<AuthorViewModel>> Create([FromQuery] AuthorCreateModel authorCreateModel) => await _authorRepository.Create(authorCreateModel);
        [HttpPut("[action]")]
        public async Task<CustomResponseModel<AuthorViewModel>> Update([FromQuery] AuthorUpdateModel authorUpdateModel) => await _authorRepository.Update(authorUpdateModel);
        [HttpDelete("[action]")]
        public async Task<CustomResponseModel<AuthorViewModel>> DeleteById(int Id) => await _authorRepository.DeleteById(Id);
    }
}