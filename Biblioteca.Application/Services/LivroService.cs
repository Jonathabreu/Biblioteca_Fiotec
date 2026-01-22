using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Dominio.Entidades;

namespace Biblioteca.Application.Services;

public class LivroService
{
    private readonly ILivroRepositorio _repo;

    public LivroService(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public async Task<Guid> CriarAsync(CriarLivroDto dto)
    {
        var livro = new Livro(dto.Titulo, dto.Autor, dto.QuantidadeInicial);
        await _repo.AdicionarAsync(livro);
        await _repo.SalvarAlteracoesAsync();
        return livro.Id;
    }

    public Task<List<Livro>> ListarAsync() => _repo.ListaAsync();

    public Task<Livro?> ObterPorIdAsync(Guid id) => _repo.ObterPorIdAsync(id);

    public async Task EditarAsync(Guid id, EditarLivroDto dto)
    {
        var livro = await _repo.ObterPorIdAsync(id)
            ?? throw new Exception("Livro não encontrado.");

        livro.AtualizarDados(dto.Titulo, dto.Autor);
        await _repo.SalvarAlteracoesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        var livro = await _repo.ObterPorIdAsync(id)
            ?? throw new Exception("Livro não encontrado.");

        await _repo.RemoverAsync(livro);
        await _repo.SalvarAlteracoesAsync();
    }
}
