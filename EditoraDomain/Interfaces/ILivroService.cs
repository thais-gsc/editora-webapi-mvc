using EditoraDomain.Entities;

namespace EditoraDomain.Interfaces
{
    public interface ILivroService
    {
        ICollection<Livro> GetAll();
        void Create(string Titulo, string ISBN, int Ano);
        void AdicionarAutor(int AutorId, int LivroId);
        Livro GetLivroById(int id);
        void Delete(int id);
        void Update(int id, string titulo, string isbn, int ano);


    }
}
