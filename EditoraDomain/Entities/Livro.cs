using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EditoraDomain.Entities
{
    public class Livro
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? ISBN { get; set; }

        public int? Ano { get; set; }

        public ICollection<Autor>? Autores { get; set; }

        public Imagem Capa { get; set; }

        public Livro()
        {
            Autores = new List<Autor>();
        }   
    }
}
