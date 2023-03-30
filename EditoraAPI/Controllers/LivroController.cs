using EditoraAPI.Service.Interfaces;
using EditoraAPI.Service.Services;
using EditoraDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EditoraAPI.Controllers
{
    [Route("livro")]
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var livro = _livroService.GetLivroById(id);
            return Ok(livro);
        }

        [HttpPost]
        [Route("novo")]
        public IActionResult Post(string Titulo, string ISBN, int Ano)
        {
            _livroService.Create(Titulo, ISBN, Ano);
            return NoContent();
        }

        [HttpPost]
        [Route("addautor")]
        public IActionResult AddAutor(int AutorId, int LivroId)
        {
            _livroService.AdicionarAutor(AutorId, LivroId);
            return NoContent();
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult Put(int id, string titulo, string isbn, int ano)
        {
            _livroService.Update(id, titulo, isbn, ano);
            return NoContent();
        }

        [HttpDelete]
        [Route("excluir")]
        public IActionResult Delete(int id)
        {
            _livroService.Delete(id);
            return NoContent();
        }

    }
}
