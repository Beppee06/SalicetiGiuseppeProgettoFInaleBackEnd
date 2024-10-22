using AppFinaleLibri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace AppFinaleLibri.Controllers
{

    [ApiController]
    [Route("[controller]")]  
    public class BookController : ControllerBase
    {
        //private static readonly 
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookController> _logger;
        public BookController(IConfiguration configuration, ApplicationDbContext context, ILogger<BookController> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetBookList")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetBookListAsync()
        {
            var sol = await _context.Books.ToListAsync();
            try
            {
                return Ok(sol);
            }
            catch (Exception e)
            {
                return NotFound("scemo io" + e.Message);
            }
        }



        [Authorize]
        [HttpPost("PostBooksFiltered")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostBooksFiltered([FromBody] SimpleBook a)
        {
            var sol = _context.Books.Where(x=> (a.Author != "" && x.Author.Contains(a.Author)) 
                                            || (a.Title != "" && x.Title.Contains(a.Title)));
            try
            {
                return Ok(sol);
            }
            catch (Exception e)
            {
                return NotFound("scemo io" + e.Message);
            }
        }



        [Authorize]
        [HttpPost("PostCreateBook")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Book))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostAsync([FromBody] SimpleBook p)
        {
            try
            {
                if (!(string.IsNullOrEmpty(p.Title) || string.IsNullOrEmpty(p.Author)))
                {
                    Book creare = new Book(p.Title, p.Author);
                    _context.Books.Add(creare);
                    await _context.SaveChangesAsync();
                    return Ok(creare);
                }
                return NotFound("non tutti i campi sono stati compilati");
            }
            catch (Exception e)
            {
                return NotFound("scemo io");
            }
        }




        [Authorize]
        [HttpPost("PostOrder")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Book))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostOrder([FromBody] SimpleBook o)
        {
            //try
            //{
                Guid Id = Guid.Parse(HttpContext.User.Identity.Name);
                if (!(string.IsNullOrEmpty(o.Title) || string.IsNullOrEmpty(o.Author)))
                {
                    Guid ordinare = await _context.Books.Where(x=> x.Title == o.Title && x.Author == o.Author).Select(x=> x.BookId).FirstAsync();
                    _context.Orders.Add(new Order(Id, ordinare));
                    await _context.SaveChangesAsync();
                    return Ok("Ordine riuscito");
                }
                return NotFound("non tutti i campi sono stati compilati");
            //}
            //catch (Exception e)
            //{
            //    return NotFound("scemo io");
            //}
        }
    }
}
