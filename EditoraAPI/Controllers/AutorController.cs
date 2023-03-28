using EditoraAPI.Service.Interfaces;
using EditoraAPI.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EditoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var autores = _autorService.GetAll();
            return Ok(autores);
        }

        //[Route("autores/novo")]
        [HttpPost]
        public IActionResult Post(string Nome, string Sobrenome, string Email, DateTime DataNascimento)
        {
            _autorService.Create(Nome, Sobrenome, Email, DataNascimento);
            return NoContent();
        }
    }
}
