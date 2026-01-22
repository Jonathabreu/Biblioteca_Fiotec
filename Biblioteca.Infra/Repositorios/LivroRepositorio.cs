using Biblioteca.Application.Interfaces;
using Biblioteca.Dominio.Entidades;
using Biblioteca.Infra.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infra.Repositorios;

public class LivroRepositorio : ILivroRepositorio
{
    private readonly BibliotecaDbContext _ctx;

    public LivroRepositorio(BibliotecaDbContext ctx)
    {
        _ctx = ctx;
    }

    public Task AdicionarAsync(Livro livro)
    {
        _ctx.Livros.Add(livro);
        return Task.CompletedTask;
    }

    public Task<Livro?> ObterPorIdAsync(Guid id)
        => _ctx.Livros.FirstOrDefaultAsync(l => l.Id == id);

    public Task<List<Livro>> ListaAsync()
        => _ctx.Livros.AsNoTracking().OrderBy(l => l.Titulo).ToListAsync();

    public Task RemoverAsync(Livro livro)
    {
        _ctx.Livros.Remove(livro);
        return Task.CompletedTask;
    }

    public Task SalvarAlteracoesAsync()
        => _ctx.SaveChangesAsync();
}
