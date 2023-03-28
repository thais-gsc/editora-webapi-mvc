using EditoraDomain.Entities;

namespace EditoraAPI.Service.Interfaces
{
    public interface ILivroService
    {
        ICollection<Livro> GetAll();
        void Create(string Titulo, string ISBN, int Ano);
        void AdicionarAutor(int AutorId, int LivroId);

    }
}
