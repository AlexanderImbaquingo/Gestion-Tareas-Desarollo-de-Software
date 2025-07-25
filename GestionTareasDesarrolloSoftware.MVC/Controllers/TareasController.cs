using GestionTareasDesarolloSoftware.API.Models;
using GestionTareasDesarrolloSoftware.APIConsumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionTareasDesarrolloSoftware.MVC.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasController
        public ActionResult Index()
        {
            var data = Crud<Tarea>.GetAll();
            return View();
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
