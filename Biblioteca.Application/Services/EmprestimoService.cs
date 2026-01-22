using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.Services;

public class EmprestimoService
{
    private readonly ILivroRepositorio _livros;
    private readonly IEmprestimoRepositorio _emprestimos;
    private readonly IUnityOfWork _uow;

    public EmprestimoService(
        ILivroRepositorio livros,
        IEmprestimoRepositorio emprestimos,
        IUnityOfWork uow)
    {
        _livros = livros;
        _emprestimos = emprestimos;
        _uow = uow;
    }

    public async Task<Guid> RegistrarEmprestimoAsync(CriarEmprestimoDto dto)
    {
        Guid idGerado = Guid.Empty;

        await _uow.ExecuteEmTransacaoAsync(async () =>
        {
            var livro = await _livros.ObterPorIdAsync(dto.LivroId)
                ?? throw new Exception("Livro não encontrado.");

            livro.BaixarEstoque();

            var emprestimo = new Emprestimo(dto.LivroId, dto.NomeSolicitante);
            await _emprestimos.AdicionarAsync(emprestimo);

            // Um único SaveChanges no final já resolve, mas pode manter separado se quiser
            await _emprestimos.SalvarAlteracoesAsync();

            idGerado = emprestimo.Id;
        });

        return idGerado;
    }

    public async Task RegistrarDevolucaoAsync(Guid emprestimoId)
    {
        await _uow.ExecuteEmTransacaoAsync(async () =>
        {
            var emprestimo = await _emprestimos.ObterPorIdComLivroAsync(emprestimoId)
                ?? throw new Exception("Empréstimo não encontrado.");

            emprestimo.RegistrarDevolucao();
            emprestimo.Livro.DevolverAoEstoque();

            await _emprestimos.SalvarAlteracoesAsync();
        });
    }
}
