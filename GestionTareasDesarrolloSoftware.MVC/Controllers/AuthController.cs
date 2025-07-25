using GestionTareasDesarrolloSoftware.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GestionTareasDesarrolloSoftware.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly string apiUrl = "https://localhost:7251/api/Usuarios"; 

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using var client = new HttpClient();
                var usuario = new
                {
                    nombre = model.nombre,
                    email = model.email,
                    passwordHash = model.password
                };
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Usuario registrado exitosamente. Ya puedes iniciar sesión.";
                    return RedirectToAction("Index", "Home");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error al registrar usuario: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error de conexión: {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel model)
        {
            // Crear un nuevo ModelState solo para email y password
            ModelState.Clear();
            
            if (string.IsNullOrEmpty(model.email))
                ModelState.AddModelError(nameof(model.email), "El email es requerido");
            
            if (string.IsNullOrEmpty(model.password))
                ModelState.AddModelError(nameof(model.password), "La contraseña es requerida");

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Por favor completa todos los campos";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                using var client = new HttpClient();
                var usuario = new
                {
                    email = model.email,
                    passwordHash = model.password
                };
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                
                var response = await client.PostAsync($"{apiUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    string token = result.token;
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Tareas");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = "Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error de conexión: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Index", "Home");
        }
    }
}