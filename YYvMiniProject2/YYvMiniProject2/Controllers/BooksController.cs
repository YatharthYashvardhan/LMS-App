using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YYvMiniProject2.Buisness.IRepository;
using YYvMiniProject2.Buisness.Repository;
using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        [Route("getBooks")]
        public async Task<IActionResult> getBooks()
    {
            var books = await _bookRepository.GetAllBooks();
            return Ok(books);
       
    }
        [HttpPost]
        [Route("addBooks")]
        public async Task<IActionResult> addBooks([FromBody] BookModel bookModel)
        {
            bookModel.Book_Id = Guid.NewGuid();
            await _bookRepository.addBooks(bookModel);
            return Ok(bookModel);
        }
        [HttpPut]
        [Route("bookBorrow/{id}")]
        public async Task<IActionResult> bookBorrow(Guid id , UserDataModel dataModel)
        {
            await _bookRepository.borrowUser(id, dataModel);
            await _bookRepository.UpdateToken(dataModel);           
            return Ok();
        }
        [HttpPut]
        [Route("returnBook/{id}")]
        public async Task<IActionResult> returnBook(Guid id, UserDataModel dataModel)
        {
            await _bookRepository.removeUser(id);
            await _bookRepository.changeToken(dataModel);
            return Ok();
        }

        //[HttpDelete]
        //[Route("deleteBook/{id}")]
        //public async Task<IActionResult> deleteBook(Guid id)
        //{
        //    var book = await _bookRepository.getBookById(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    await _bookRepository.deleteBookById(id);
        //    return Ok(book);

        //}

    }
}
