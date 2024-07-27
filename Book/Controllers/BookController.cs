using Book.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;


namespace Author.Controllers
{
    [ApiController]
    [Route("api/book/")]
    public class BookController : ControllerBase
    {
        private static List<BookModel> Books = new List<BookModel>
        {
            new BookModel { Id = 1, Title = "Book 1", AuthorId = 1 },
            new BookModel { Id = 2, Title = "Book 2", AuthorId = 2 },
            new BookModel { Id = 3, Title = "Book 3", AuthorId = 3 },
            new BookModel { Id = 4, Title = "Book 4", AuthorId = 4 },
            new BookModel { Id = 5, Title = "Book 5", AuthorId = 5 }
        };

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetBooks()
        {
            return Ok(Books.OrderBy(b =>b.Id));
        }

        [HttpGet]
        [Route("GetBookById/{id}")]

        public IActionResult GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Route("CreateBook")]

        public IActionResult AddBook([FromBody] BookModel newBook)
        {
            if (newBook == null)
                return BadRequest("Book cannot be null.");

            if (Books.Any(b => b.Id == newBook.Id))
                return Conflict("A book with this ID already exists.");

            Books.Add(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = Books.Find(b => b.Id == id);
            if (book == null)
                return NotFound($"Not found book has id {id}");

            Books.Remove(book);
            return Ok("Delete success");
        }

        [HttpPut]
        [Route("UpdateBook/{id}")]
        public IActionResult UpdateBook(int id, BookModel updateBook)
        {
            var book = Books.Find(b => b.Id == id);
            if (book == null)
                return NotFound($"Not found book has id {id}");
            if (updateBook == null)
                return BadRequest("Book cannot be null");

            book.Title=updateBook.Title;
            book.AuthorId=updateBook.AuthorId;
            return Ok(book);
        }


    }
}
