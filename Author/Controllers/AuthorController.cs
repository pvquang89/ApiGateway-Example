using Author.Model;
using Microsoft.AspNetCore.Mvc;

namespace Author.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private static List<AuthorModel> Authors = new List<AuthorModel>
    {
        new AuthorModel { Id = 1, Name = "Author 1" },
        new AuthorModel { Id = 2, Name = "Author 2" },
        new AuthorModel { Id = 3, Name = "Author 3" },
        new AuthorModel { Id = 4, Name = "Author 4" },
        new AuthorModel { Id = 5, Name = "Author 5" }
    };

        [HttpGet]
        [Route("GetAllAuthor")]
        public IActionResult GetAuthors()
        {
            return Ok(Authors);
        }

        [HttpGet]
        [Route("GetAuthorById/{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
    }
}
