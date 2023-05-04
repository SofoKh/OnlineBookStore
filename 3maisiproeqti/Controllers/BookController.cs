using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Models;
using _3maisiproeqti.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3maisiproeqti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<List<BookViewModel>>> GetAll() => await _bookRepository.GetAll();
        [HttpGet("[action]")]
        public async Task<CustomResponseModel<BookViewModel>> GetById(int Id) => await _bookRepository.GetById(Id);
        [HttpPost("[action]")]
        public async Task<CustomResponseModel<BookViewModel>> Create([FromQuery] BookCreateModel bookCreateModel) => await _bookRepository.Create(bookCreateModel);
        [HttpPut("[action]")]
        public async Task<CustomResponseModel<BookViewModel>> Update([FromQuery] BookUpdateModel bookUpdateModel) => await _bookRepository.Update(bookUpdateModel);
        [HttpDelete("[action]")]
        public async Task<CustomResponseModel<BookViewModel>> DeleteById(int Id) => await _bookRepository.DeleteById(Id);
    }
}
