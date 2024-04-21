using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcNbaApis.Extensions;
using MvcNbaApis.Models;
using MvcNbaApis.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcNbaApis.Controllers
{
    public class UsuariosController : Controller
    {
        private ServiceUsuarios serviceUsuarios;

        public UsuariosController(ServiceUsuarios serviceUsuarios)
        {
            this.serviceUsuarios = serviceUsuarios;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool loginSuccessful = await serviceUsuarios.LoginAsync(model);
                if (loginSuccessful)
                {
                    // Redirigir a la página principal o a otra página después del inicio de sesión
                    Usuario user = await this.serviceUsuarios.VerPerfilAsync(model.UserName);
                    HttpContext.Session.SetObject("CurrentUser", user);
                    return RedirectToAction("PartidosJugados", "Nba");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                }
            }
            return RedirectToAction("VerPerfil");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegisterModel model)
        {
          bool registrationSuccessful = await serviceUsuarios.RegisterAsync(model);
                if (registrationSuccessful)
                {
                    Usuario user = await this.serviceUsuarios.VerPerfilAsync(model.UserName);
                    HttpContext.Session.SetObject("CurrentUser", user);
                    return RedirectToAction("PartidosJugados", "Nba");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario. Inténtelo de nuevo más tarde.");
                }
            
                return View(model);
        }

        public async Task<IActionResult> VerPerfil(string username)
        {
            Usuario usuario = await serviceUsuarios.VerPerfilAsync(username);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        public async Task<IActionResult> EditarPerfil(string username)
        {
            Usuario usuario = await serviceUsuarios.VerPerfilAsync(username);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> EditarPerfil(string username, Usuario usuario)
        {

            bool editSuccessful = await serviceUsuarios.EditarPerfilAsync(username, usuario);
            if (editSuccessful)
            {
                // Redirigir a la página de perfil después de la edición exitosa
                return RedirectToAction("Perfil", new { username = username });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al editar el perfil. Inténtelo de nuevo más tarde.");
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool deleteSuccessful = await serviceUsuarios.DeleteAsync(id);
            if (deleteSuccessful)
            {
                // Redirigir a la página principal o a otra página después de eliminar el usuario
                return RedirectToAction("PartidosJugados", "Nba");
            }
            else
            {
                // Manejar el error de eliminación de usuario
                return StatusCode(500, "Error al eliminar el usuario. Inténtelo de nuevo más tarde.");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Login");
        }
    }
}
