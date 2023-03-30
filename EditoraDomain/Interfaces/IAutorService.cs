using EditoraDomain.Entities;

namespace EditoraAPI.Service.Interfaces
{
    public interface IAutorService
    {
        ICollection<Autor> GetAll();
        void Create(string Nome, string Sobrenome, string Email, DateTime DataNascimento);
        Autor GetAutorById(int id);
        void Delete(int id);
        void Update(int id, string Nome, string Sobrenome, string Email, DateTime DataNascimento);
    }
}
