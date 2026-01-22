using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.Interfaces
{
    public interface IEmprestimoRepositorio
    {
        Task AdicionarAsync(Emprestimo emprestimo);
        Task<Emprestimo?> ObterPorIdComLivroAsync(Guid id);
        Task<List<Emprestimo>> ListarPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task SalvarAlteracoesAsync();
    }
}
