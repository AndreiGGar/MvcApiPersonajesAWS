using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;
using System.Diagnostics;

namespace MvcApiPersonajesAWS.Controllers
{
    public class HomeController : Controller
    {

        private ServiceApiPersonajes service;

        public HomeController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            ViewData["TEST"] = await this.service.TestApiAsync();
            return View();
        }

        public async Task<IActionResult> ApiPersonajes()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.GetPersonajeByIdAsync(id);
            return View(personaje);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string imagen)
        {
            await this.service.CreatePersonajeAsync(nombre, imagen);
            return RedirectToAction("ApiPersonajes");
        }

        public async Task<IActionResult> Update(int id)
        {
            Personaje personaje = await this.service.GetPersonajeByIdAsync(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string nombre, string imagen)
        {
            await this.service.UpdatePersonajeAsync(id, nombre, imagen);
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("ApiPersonajes"); ;
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