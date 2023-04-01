using EditoraDomain.Entities;
using EditoraDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EditoraMVC.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        public IActionResult Index()
        {
            var autores = _autorService.GetAll();
            return View(autores);
        }

        public IActionResult Details(int id)
        {
            if (id == null || _autorService.GetAutorById(id) == null)
            {
                return NotFound();
            }

            var autor = _autorService.GetAutorById(id);

            return View(autor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Nome, string Sobrenome, string Email, DateTime DataNascimento)
        {
            _autorService.Create(Nome, Sobrenome, Email, DataNascimento);
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == null || _autorService.GetAutorById(id) == null)
            {
                return NotFound();
            }

            var autor = _autorService.GetAutorById(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost]
        public IActionResult Edit(int id, string Nome, string Sobrenome, string Email, DateTime DataNascimento)
        {
            _autorService.Update(id, Nome, Sobrenome, Email, DataNascimento);

            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == null || _autorService.GetAutorById(id) == null)
            {
                return NotFound();
            }

            var autor = _autorService.GetAutorById(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _autorService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }

}
