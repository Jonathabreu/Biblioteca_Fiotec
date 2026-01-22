using Biblioteca.Dominio.Entidades;

namespace Biblioteca.Domain.Entities
{
    public class Emprestimo
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid LivroId { get; private set; }
        public Livro Livro { get; private set; } = null!;

        public string NomeSolicitante { get; private set; } = "";
        public DateTime DataEmprestimo { get; private set; }
        public DateTime? DataDevolucao { get; private set; }

        private Emprestimo() { }

        public Emprestimo(Guid livroId, string nomeSolicitante)
        {
            if(livroId == Guid.Empty)
                throw new ArgumentException("LivroId é obrigatório.");
            if (string.IsNullOrWhiteSpace(nomeSolicitante))
                throw new ArgumentException("Nome do solicitante é obrigatório.");
            LivroId = livroId;
            NomeSolicitante = nomeSolicitante.Trim();
            DataEmprestimo = DateTime.UtcNow;
        }

        public void RegistrarDevolucao()
        {
            if (DataDevolucao != null)
                throw new InvalidOperationException("Empréstimo já foi devolvido.");
            DataDevolucao = DateTime.UtcNow;
        }
    }
}
