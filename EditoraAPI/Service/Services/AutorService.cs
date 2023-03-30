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

        public Autor GetAutorById(int id)
        {
            return _dbContext.autores.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var autor = _dbContext.autores.FirstOrDefault(x => x.Id == id);

            this._dbContext.autores.Remove(autor);
            this._dbContext.SaveChanges();
        }

        public void Update(int id, string nome, string sobrenome, string email, DateTime dataNascimento)
        {
            var autor = _dbContext.autores.FirstOrDefault(x => x.Id == id);

            autor.Nome = nome;
            autor.Sobrenome = sobrenome;
            autor.Email = email;
            autor.DataNascimento = dataNascimento;

            _dbContext.autores.Update(autor);
            this._dbContext.SaveChanges();

        }
    }
}
