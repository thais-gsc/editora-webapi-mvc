using EditoraAPI.Service.Interfaces;
using EditoraDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EditoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        [Route("/livros")]
        public IActionResult Get()
        {
            var livros = _livroService.GetAll();
            return Ok(livros);
        }

        [HttpPost]
        [Route("livros/novo")]
        public IActionResult Post(string Titulo, string ISBN, int Ano)
        {
            _livroService.Create(Titulo, ISBN, Ano);
            return NoContent();
        }

        [HttpPost]
        [Route("livros/addautor")]
        public IActionResult AddAutor(int AutorId, int LivroId)
        {
            _livroService.AdicionarAutor(AutorId, LivroId);
            return NoContent();
        }
    }
}
