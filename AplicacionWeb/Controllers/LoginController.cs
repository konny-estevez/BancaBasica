using AplicacionWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AplicacionWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<InicioSesionModel> _logger;

        public LoginController(SignInManager<IdentityUser> signInManager, ILogger<InicioSesionModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var returnUrl = Url.Content("~/");
            // Clear the existing external cookie to ensure a clean login process
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new InicioSesionModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(InicioSesionModel modelo, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(modelo.NombreUsuario, modelo.Contrasena, modelo.Recordar, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Uusuario ha iniciado sesión.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = modelo.Recordar});
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Cuenta de usuario bloqueada.");
                    return RedirectToPage("/Identity/Account/Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión inválido.");
                    return View(modelo);
                }
            }

            return View(modelo);
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuario a cerrado sesión.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}
