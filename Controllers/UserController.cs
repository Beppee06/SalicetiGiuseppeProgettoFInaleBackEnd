using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using esDef.Models;

namespace esDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public UserController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }



        [HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> LoginAsync([FromBody] SimpleUser u)
        {
            User check = _context.Users.Where(x=> x.Email == u.Email).FirstOrDefault();
            if (check == null)
                return NotFound("Account non trovato");
            if (u.Password == check.Password)
            {
                //legge la configurazione di TokenOptions
                var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOption>();
                //prende secret
                var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //configurazione del JWT:
                    //utente
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, check.Id.ToString())
                    }),
                    //Issuer: colui che ha creato il token
                    Issuer = tokenOptions.Issuer,
                    //Audience: chi utilizzera questo token, cioè quali sono server e API 
                    Audience = tokenOptions.Audience,
                    //scadenza
                    Expires = DateTime.UtcNow.AddDays(tokenOptions.ExpiryDays),
                    //algoritmo di generazione della firma, serve per controllare che la token hai creato tu
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                //seguente codice è per creare token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }

            return NotFound("Password sbagliata");
        }




        [HttpPost("Register")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> RegisterAsync([FromBody] SimpleUser u)
        {
            // Simulate user authentication (replace with actual authentication logic)
            User check = new User(u.Email, u.Password);
            if (!(string.IsNullOrEmpty(u.Email) || string.IsNullOrEmpty(u.Password)))
            {
                //return NotFound(u);//per controllare perche' mi dava errore
                _context.Users.Add(check);
                await _context.SaveChangesAsync();
                //legge la configurazione di TokenOptions
                var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOption>();
                //prende sicret
                var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //configurazione del JWT:
                    //utente
                    Subject = new ClaimsIdentity(new[]
                    {
            new Claim(ClaimTypes.Name, u.Email)
            }),
                    //Issuer: colui che ha creato il token
                    Issuer = tokenOptions.Issuer,
                    //Audience: chi utilizzera questo token, cioè quali sono server e API 
                    Audience = tokenOptions.Audience,
                    //scadenza
                    Expires = DateTime.UtcNow.AddDays(tokenOptions.ExpiryDays),
                    //algoritmo di generazione della firma, serve per controllare che la token hai creato tu
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                //seguente codice è per creare token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) } );
            }

            return NotFound("Non tutti i campi sono stati non compilati");
        }
    }
}
