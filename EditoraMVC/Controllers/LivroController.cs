using EditoraDomain.Entities;
using EditoraDomain.Interfaces;
using EditoraService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EditoraMVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        public IActionResult Index()
        {
            var livros = _livroService.GetAll();
            return View(livros);
        }

        public IActionResult Details(int id)
        {
            if (id == null || _livroService.GetLivroById(id) == null)
            {
                return NotFound();
            }

            var livro = _livroService.GetLivroById(id);

            return View(livro);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Titulo, string ISBN, int Ano)
        {
            _livroService.Create(Titulo, ISBN, Ano);
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == null || _livroService.GetLivroById(id) == null)
            {
                return NotFound();
            }

            var livro = _livroService.GetLivroById(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult Edit(int id, string Titulo, string ISBN, int Ano)
        {
            _livroService.Update(id, Titulo, ISBN, Ano);

            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == null || _livroService.GetLivroById(id) == null)
            {
                return NotFound();
            }

            var livro = _livroService.GetLivroById(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _livroService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddAutor(int id)
        {
            if (id == null || _livroService.GetLivroById(id) == null)
            {
                return NotFound();
            }

            var livro = _livroService.GetLivroById(id);

            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult AddAutor(int id,int AutorId)
        {
            _livroService.AdicionarAutor(id, AutorId);
            return Ok();
            
        }
    }
}
