namespace EditoraDomain.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        public ICollection<Livro>? Livros { get; set; }
    }
}