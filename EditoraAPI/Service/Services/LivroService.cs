using EditoraAPI.Service.Interfaces;
using EditoraDomain.Entities;
using EditoraService;
using Microsoft.EntityFrameworkCore;

namespace EditoraAPI.Service.Services
{
    public class LivroService: ILivroService
    {
        private readonly EditoraDbContext _dbContext;
        public LivroService(EditoraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AdicionarAutor(int AutorId, int LivroId)
        {
            var livroDb = _dbContext.livros.First(l => l.Id == LivroId);
            var autorDb = _dbContext.autores.First(a => a.Id == AutorId);
            livroDb.Autores.Add(autorDb);
            _dbContext.SaveChanges();
        }

        public void Create(string Titulo, string ISBN, int Ano)
        {
            var livro = new Livro()
            {
                Titulo = Titulo,
                ISBN = ISBN,
                Ano = Ano
            };

            _dbContext.livros.Add(livro);
            _dbContext.SaveChanges();
        }

        public ICollection<Livro> GetAll()
        {
            return _dbContext.livros
                .Include(l => l.Autores)
                .ToList();
        }

        public Livro GetLivroById(int id)
        {
            return _dbContext.livros.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var livro = _dbContext.livros.FirstOrDefault(x => x.Id == id);

            this._dbContext.livros.Remove(livro);
            this._dbContext.SaveChanges();
        }

        public void Update(int id, string titulo, string isbn, int ano)
        {
            var livro = _dbContext.livros.FirstOrDefault(x => x.Id == id);

            livro.Titulo = titulo;
            livro.ISBN = isbn;
            livro.Ano = ano;

            _dbContext.livros.Update(livro);
            this._dbContext.SaveChanges();

        }
    }
}
