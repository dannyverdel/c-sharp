namespace WebAPIAuthorizationDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private readonly IConfiguration _configuration;
    private readonly IUserService _user_service;

    public AuthController(IConfiguration configuration, IUserService user_service) {
        _configuration = configuration;
        _user_service = user_service;
    }

    [HttpGet("user"), Authorize]
    public ActionResult<string> GetMe() => Ok(_user_service.GetMyName());

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request) {
        string password_hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.Username = request.Username;
        user.Roles = request.Roles;
        user.PasswordHash = password_hash;

        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request) {
        if ( user.Username != request.Username )
            return BadRequest("User not found");

        if ( !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) )
            return Unauthorized("Wrong password");

        string token = CreateToken(user);

        return Ok(token);
    }

    private string CreateToken(User user) {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username)
        };

        foreach ( string role in user.Roles )
            claims.Add(new Claim(ClaimTypes.Role, role));

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}

