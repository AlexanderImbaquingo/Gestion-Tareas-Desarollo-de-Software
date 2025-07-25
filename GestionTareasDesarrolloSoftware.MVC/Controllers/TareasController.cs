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
            return View(data);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Tarea>.GetById(id);
            return View(data);
        }

        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea data)
        {
            try
            {
                Crud<Tarea>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Tarea>.GetById(id);
            return View(data);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea data)
        {
            try
            {
                Crud<Tarea>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Tarea>.GetById(id);
            return View(data);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea data)
        {
            try
            {
                Crud<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }
        
        public ActionResult Reporte(string estado, string orden = "FechaLimite")
        {
            var tareas = Crud<Tarea>.GetAll();

            if (!string.IsNullOrEmpty(estado))
                tareas = tareas.Where(t => t.estado == estado).ToList();

            tareas = orden switch
            {
                "FechaLimite" => tareas.OrderBy(t => t.FechaLimite).ToList(),
                "FechaInicio" => tareas.OrderBy(t => t.FechaInicio).ToList(),
                _ => tareas
            };

            return View(tareas);
        }
        
        public ActionResult BuscarAgrupado(string agruparPor = "proyecto")
        {
            var tareas = Crud<Tarea>.GetAll();
            if (agruparPor == "usuario")
                return View("BuscarPorUsuario", tareas.GroupBy(t => t.usuarioId));
            else
                return View("BuscarPorProyecto", tareas.GroupBy(t => t.proyectoId));
        }
    }
}
