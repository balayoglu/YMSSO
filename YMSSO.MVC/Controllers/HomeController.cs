using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YMDiscourseSSO.Models;
using YMDiscourseSSO.Services;

namespace YMDiscourseSSO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IYourMembershipService yourMembershipService;

        public HomeController(
            ILogger<HomeController> logger,
            IYourMembershipService yourMembershipService)
        {
            _logger = logger;
            this.yourMembershipService = yourMembershipService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            //var sso = Request.Query["sso"];
            //var sig = Request.Query["sig"];
            //if(string.IsNullOrWhiteSpace(sso) || string.IsNullOrWhiteSpace(sig))
            //{
            //    ModelState.AddModelError("Error", "Discourse SSO or SIG query string parameter(s) is/are missing!");
            //    return View("Index");
            //}

            var tokenResult = this.yourMembershipService.GetYmTokenResult(loginModel.UserName, loginModel.Password);
            if(loginModel.Redirect)
            {
                return Redirect(tokenResult.GotoUrl);
            }

            ViewBag.Message = tokenResult.GotoUrl;
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
