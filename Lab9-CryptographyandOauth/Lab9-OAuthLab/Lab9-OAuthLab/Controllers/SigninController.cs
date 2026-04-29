using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
namespace Lab9_OAuthLab.Controllers
{
    public class SigninController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties()
            {
                RedirectUri = "/signin/index"
            }, "Microsoft");
        }
    }
}
