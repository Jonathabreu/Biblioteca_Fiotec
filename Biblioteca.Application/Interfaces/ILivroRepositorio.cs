using Biblioteca.Dominio.Entidades;

namespace Biblioteca.Application.Interfaces
{
    public interface ILivroRepositorio
    {
        Task AdicionarAsync(Livro livro);
        Task<Livro?> ObterPorIdAsync(Guid id);
        Task<List<Livro>> ListaAsync();
        Task RemoverAsync(Livro livro);
        Task SalvarAlteracoesAsync();
    }
}
