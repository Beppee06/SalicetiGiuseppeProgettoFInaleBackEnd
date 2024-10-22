using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using esDef.Models;
using Microsoft.AspNetCore.Authorization;


namespace AppFinaleLibri.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public OrderController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }




        [Authorize]
        [HttpGet("GetOrders")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                Guid Id = Guid.Parse(HttpContext.User.Identity.Name);
                var sol = _context.Orders.Where(x => x.UserId == Id).Select(x => new
                {
                    Id = x.Id,
                    BookId = x.BookId,
                });
                return Ok(sol);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
