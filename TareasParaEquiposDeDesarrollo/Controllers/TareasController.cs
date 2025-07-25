using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Consumer;
using SistemaEquiposDesarrollo.Modelos;

namespace TareasParaEquiposDeDesarrollo.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        // GET: TareasController
        public ActionResult Index()
        {
            var tareas = Crud<Tarea>.GetAll();
            return View(tareas);
        }

        // GET: TareasController/Details/5
        public ActionResult Details(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            return View(tarea);
        }


        // GET: TareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        private List<SelectListItem> GetProyectos()
        {
            var proyectos = Crud<Proyecto>.GetAll();
            return proyectos.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Nombre
            }).ToList();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Create(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }

        // GET: TareasController/Edit/5
        public ActionResult Edit(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Update(id, tarea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }

        // GET: TareasController/Delete/5
        public ActionResult Delete(int id)
        {
            var tarea = Crud<Tarea>.GetById(id);
            return View(tarea);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarea tarea)
        {
            try
            {
                Crud<Tarea>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }

        public ActionResult Reportes(string estado)
        {
            var tareas = Crud<Tarea>.GetAll();

            tareas = tareas.Where(t => estado == null || t.Estado == estado).ToList();
            var proyectos = Crud<Proyecto>.GetAll();

            return View(tareas);
        }
    }
}
