using EditoraAPI.Service.Interfaces;
using EditoraAPI.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EditoraAPI.Controllers
{
    [Route("autor")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        [Route("/autores")]
        public IActionResult Get()
        {
            var autores = _autorService.GetAll();
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var autor = _autorService.GetAutorById(id);
            return Ok(autor);
        }

        [HttpPost]
        [Route("novo")]
        public IActionResult Post(string Nome, string Sobrenome, string Email, DateTime DataNascimento)
        {
            _autorService.Create(Nome, Sobrenome, Email, DataNascimento);
            return NoContent();
        }

        [HttpDelete]
        [Route("excluir")]
        public IActionResult Delete(int id)
        {
            _autorService.Delete(id);
            return NoContent();
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult Put(int id, string nome, string sobrenome, string email, DateTime dataNascimento)
        {
            _autorService.Update(id, nome, sobrenome, email, dataNascimento);
            return NoContent();
        }
    }
}
