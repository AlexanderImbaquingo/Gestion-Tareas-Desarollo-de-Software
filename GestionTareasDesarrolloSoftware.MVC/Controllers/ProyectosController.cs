using GestionTareasDesarolloSoftware.API.Models;
using GestionTareasDesarrolloSoftware.APIConsumer;
using GestionTareasDesarrolloSoftware.MVC.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionTareasDesarrolloSoftware.MVC.Controllers
{
    [AuthRequired]
    public class ProyectosController : Controller
    {
        // GET: ProyectosController
        public ActionResult Index()
        {
            var data = Crud<Proyecto>.GetAll();
            return View(data);
        }

        // GET: ProyectosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProyectosController/Create
        public ActionResult Create()
        {
            var usuarios = Crud<Usuario>.GetAll();
            ViewBag.Usuarios = new SelectList(usuarios, "id", "nombre");
            return View();
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proyecto proyecto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Crud<Proyecto>.Create(proyecto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            var usuarios = Crud<Usuario>.GetAll();
            ViewBag.Usuarios = new SelectList(usuarios, "id", "nombre");
            return View(proyecto);
        }

        // GET: ProyectosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Proyecto>.GetById(id);
            return View(data);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proyecto data)
        {
            try
            {
                Crud<Proyecto>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: ProyectosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Proyecto>.GetById(id);  
            return View();
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proyecto data)
        {
            try
            {
                Crud<Proyecto>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }
    }
}
