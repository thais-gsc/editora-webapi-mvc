using EditoraAPI.Service.Interfaces;
using EditoraDomain.Entities;
using EditoraService;
using Microsoft.EntityFrameworkCore;

namespace EditoraAPI.Service.Services
{
    public class AutorService: IAutorService
    {
        private readonly EditoraDbContext _dbContext;
        public AutorService(EditoraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(string Nome, string Sobrenome, string Email, DateTime DataNascimento)
        {
            var autor = new Autor()
            {
                Nome = Nome,
                Sobrenome = Sobrenome,
                Email = Email,
                DataNascimento = DataNascimento
            };
            _dbContext.autores.Add(autor);
            _dbContext.SaveChanges();
        }

        public ICollection<Autor> GetAll()
        {
            return _dbContext.autores.Include(a => a.Livros).ToList();
        }
    }
}
