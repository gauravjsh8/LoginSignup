using LoginSignup.Data;
using LoginSignup.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginSignup.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)


        {
            _userManager = userManager;
            _signInManager = signInManager;



        }
         
        public IActionResult RegisterUser()

        {
            return View();
        }
        public async Task<IActionResult> RegisterUsers(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser()
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };
            var userCreateResult = await _userManager.CreateAsync(user, registerViewModel.Password);
           

                if (userCreateResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("LoginUser");
                }
            
            else
            {

                foreach (var item in userCreateResult.Errors)
                {

                    ModelState.AddModelError(string.Empty, "Error!" + item.Description);



                    return View("RegisterUser", registerViewModel);
                }
            }

            return RedirectToAction("RegisterUser");
        }
        public IActionResult LoginUser()

        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("LoginUser");
        }
        public async Task<IActionResult> LoginUsers(LoginViewModel loginViewModel)
        {
            var identityResult = await _signInManager.
                PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            if (identityResult.Succeeded)
            {
                 return RedirectToAction("Index", "Home");
                 
            }
            return RedirectToAction("LoginUser");
        }
    }
}
