namespace Library_Api.Controllers
{
    using Library_Domain.Model;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
                private readonly SignInManager<IdentityUser> _signInManager;

                private readonly UserManager<IdentityUser> _userManager;

                private readonly JwtSettings _jwtSettings;

                public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

                [HttpPost("register")]

        public async Task<IActionResult> Register(RegisteruserViewModel registeruserViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registeruserViewModel.Email,
                Email = registeruserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registeruserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok("Usuário registrado!");
            }
            return Problem("User registration failed");
        }

                [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false, true);
            if (result.Succeeded)
            {
                return Ok(GerarToken());
            }
            return Problem("User login failed");
        }

                protected string GerarToken()
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenCreated = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenCreated);
        }
    }
}
