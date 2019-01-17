using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EscalonamentoHospitalar.Models;
using EscalonamentoHospitalar.Data;
using EscalonamentoHospitalar.Controllers;

namespace EscalonamentoHospitalar.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        private readonly HospitalDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<ApplicationUser> signInManager,
                            ILogger<AccountController> logger,
                            HospitalDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;

            UsersSeedData.EnsurePopulatedAsync(userManager, roleManager).Wait();
        }

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        //public static IdentityUser Codigo { get; private set; }
        //public static bool SignInAs { get; private set; }

        //public class InputModel
        //{
        //    [Required(ErrorMessage = "Por favor, insira o código")]
        //    [RegularExpression(@"[MDE]\d+", ErrorMessage = "Código inválido")]
        //    public string Codigo { get; set; }

        //    [Required(ErrorMessage = "Por favor, insira a password")]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [Required]
        //    public string SignInAs { get; set; }
        //}

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if (!string.IsNullOrEmpty(ErrorMessage))
        //    {
        //        ModelState.AddModelError(string.Empty, ErrorMessage);
        //    }

        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    Clear the existing external cookie to ensure a clean login process
        //   await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    ReturnUrl = returnUrl;
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAccount model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Codigo, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToAction(nameof(Lockout));
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,NumeroMecanografico,Nome,Email,Codigo")] UserAccount userAccount, UserAccount model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = model.Codigo, Email = model.Email };
                userAccount = new UserAccount
                {
                    NumeroMecanografico = model.NumeroMecanografico,
                    Nome = model.Nome,
                    Email = model.Email,
                    Codigo = model.Codigo

                };
                var result = await _userManager.CreateAsync(user, model.Password);
               
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Administrador"); 
                                                                     

                    _context.Add(userAccount);
                    await _context.SaveChangesAsync();

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }

              //  AddErrors(result);
                
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(AccountController.Codigo, Input.Password, AccountController.SignInAs, lockoutOnFailure: true);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User logged in.");
        //            return LocalRedirect(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.SignInAs });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning("User account locked out.");
        //            return RedirectToPage("./Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return Page();
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}
    }
}
