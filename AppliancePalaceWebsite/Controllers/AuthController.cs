using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AppliancePalaceWebsite.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterRequestModel request)
        {
            User? userFromDb = await _userRepository.GetByMail(request.Email);

            if (userFromDb != null)
                return BadRequest("Email Is Alreay Used");

            User user = new()
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                City = request.City,
                Address = request.Address
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Role = RoleEmun.User;

            await _userRepository.Add(user);

            string token = _jwtProvider.GenrateAccessToken(user);

            CookieOptions options = new()
            {
                Expires = DateTime.UtcNow.AddDays(1)
            };

            Response.Cookies.Append("AccessToken", token, options);
            // Modify Redirect
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginRequestModel request)
        {
            User? userFromDb = await _userRepository.GetByMail(request.Email);

            if (userFromDb == null)
                return NotFound();

            if (!BCrypt.Net.BCrypt.Verify(request.Password, userFromDb.Password))
                return BadRequest("Worng Password");

            string token = _jwtProvider.GenrateAccessToken(userFromDb);

            CookieOptions options = new()
            {
                Expires = DateTime.UtcNow.AddDays(1)
            };

            Response.Cookies.Append("AccessToken", token, options);
            // Modify Redirect
            return View(userFromDb);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");
            // Modify Redirect
            return View();
        }
    }
}
