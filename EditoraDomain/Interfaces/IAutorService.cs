using EditoraDomain.Entities;

namespace EditoraAPI.Service.Interfaces
{
    public interface IAutorService
    {
        ICollection<Autor> GetAll();
        void Create(string Nome, string Sobrenome, string Email, DateTime DataNascimento);
    }
}
