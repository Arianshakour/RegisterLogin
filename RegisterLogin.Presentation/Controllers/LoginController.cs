using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Application.Services.Interfaces;
using RegisterLogin.Domain.Common.UtilityOriginal;
using RegisterLogin.Domain.ViewModels;
using System.Security.Claims;

namespace RegisterLogin.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                //return View(register);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/Register.cshtml", register);
                return Json(new { view = v1, isGrid = 1, message = "لطفا اطلاعات فرم را کامل و صحیح وارد کنید." });
            }
            if (_loginService.IsExistEmail(register.Email))
            {
                //ModelState.AddModelError("Email", "ایمیل معتبر نیست");
                //return View(register);
                return Json(new { success = false, message = "ایمیل وارد شده در سیستم وجود دارد" });
            }
            if (_loginService.IsExistUserName(register.UserName))
            {
                return Json(new { success = false, message = "نام کاربری وارد شده در سیستم وجود دارد" });
            }
            _loginService.AddUser(register);
            //return PartialView("SuccessRegister", register);
            //return Json(new { success = true });
            var htmlContent = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/SuccessRegister.cshtml", register);
            return Json(new { success = true, html = htmlContent, register });
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                //return View(login);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/Login.cshtml", login);
                return Json(new { view = v1, isGrid = 1, message = "لطفا اطلاعات فرم را کامل و صحیح وارد کنید." });
            }
            var user = _loginService.LoginMember(login);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات یافت شده پیدا نشد");
                //return View(login);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/Login.cshtml", login);
                return Json(new { view = v1, isGrid = 1, message = "کاربری با مشخصات یافت شده پیدا نشد." });
            }
            if (user.IsActive == false)
            {
                ModelState.AddModelError("Email", "حسابت فعال نیست");
                //return View(login);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/Login.cshtml", login);
                return Json(new { view = v1, isGrid = 1, message = "کاربری با مشخصات یافت شده پیدا نشد." });
            }
            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email,user.Email)
                    };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);
            ViewBag.IsSuccess = true;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult LoadLogout()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login/Login");
        }
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/Views/Login/ForgotPassword.cshtml");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                //return View(forgot);
                return Json(new { success = false, message = "لطفا ایمیل را وارد کنید." });
            }
            if (!_loginService.IsExistEmail(forgot.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نیست");
                //return View(forgot);
                return Json(new { success = false, message = "ایمیل وارد شده معتبر نیست." });
            }
            //return RedirectToAction("ResetPassword", new { email = forgot.Email });
            var htmlContent = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/ForgotPasswordStep2.cshtml", new ForgotPasswordViewModel { Email = forgot.Email });
            return Json(new { success = true, html = htmlContent, email = forgot.Email });
        }
        public IActionResult VerifyCode(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }
            return PartialView(new ForgotPasswordViewModel { Email = email });
        }
        [HttpPost]
        public IActionResult VerifyCode(ForgotPasswordViewModel forgot)
        {
            if (forgot.VerificationCode == 0)
            {
                return Json(new { success = false, message = "لطفا کد تایید را وارد کنید" });
            }
            if (forgot.VerificationCode != 11111)
            {
                return Json(new { success = false, message = "کد وارد شده صحیح نیست" });
            }
            var htmlContent = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/ResetPassword.cshtml", new ResetPasswordViewModel { Email = forgot.Email });
            return Json(new { success = true, html = htmlContent, email = forgot.Email });
        }
        public IActionResult ResetPassword(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }
            return PartialView(new ResetPasswordViewModel { Email = email });
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                //return View(reset);
                return Json(new { success = false, message = "لطفا اطلاعات را کامل وارد کنید." });
            }
            _loginService.UpdateUser(reset);
            //return RedirectToAction("Login", "Login");
            var htmlContent = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/Login.cshtml", new LoginViewModel());
            return Json(new { success = true, html = htmlContent });
        }
        public IActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var model = _loginService.changePassword(email);
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                //return View(change);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/ChangePassword.cshtml", change);
                return Json(new { view = v1, isGrid = 1, message = "لطفا اطلاعات فرم را کامل و صحیح وارد کنید." });
            }
            if (!_loginService.IsCorrectPassword(change.Email, change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "رمز فعلی اشتباه است");
                //return View(change);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/ChangePassword.cshtml", change);
                return Json(new { view = v1, isGrid = 1, message = "کاربری با مشخصات یافت شده پیدا نشد." });
            }
            _loginService.UpdatePassword(change);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
