using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Security.Claims;
namespace Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login(string username, string password)
        {
            var users = _userRepository.GetAll();
            User user = null;

            foreach (var u in users)
            {
                if (u.Username == username && u.Password == password)
                {
                    user = u;
                    break;
                }
            }

            if (user != null)
            {
                // Set cookies for username and userId
                Response.Cookies.Append("username", username, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });
                Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                var users = _userRepository.GetAll();
                bool userExists = false;

                foreach (var u in users)
                {
                    if (u.Username == user.Username)
                    {
                        userExists = true;
                        break;
                    }
                }

                if (!userExists)
                {
                    _userRepository.Add(user);
                    // Set cookies for username and userId
                    Response.Cookies.Append("username", user.Username, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                    Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("", "Username already exists.");
            }
            return View(user);
        }
        public IActionResult Logout()
        {
            // Delete cookies for username and userId
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
            {
                return RedirectToAction("Login", "User");
            }
            //int userId = int.Parse(userIdStr);
            var user = _userRepository.GetByGuid(userIdStr);
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewData["IsLoggedIn"] = true;
            ViewData["Username"] = user.Username;
            return View(user);
        }
    }
}