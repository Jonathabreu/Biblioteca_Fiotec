using Biblioteca.Application.Interfaces;

namespace Biblioteca.Application.Services
{
    public class RelatorioService
    {
        private readonly ILivroRepositorio _livros;

        public RelatorioService(ILivroRepositorio livros)
        {
            _livros = livros;
        }

        public async Task<List<object>> LivrosComBaixoEstoqueAsync()
        {
            var lista = await _livros.ListaAsync();

            return lista
                .Where(l => l.QuantidadeDisponivel < 3)
                .OrderBy(l => l.QuantidadeDisponivel)
                .ThenBy(l => l.Titulo)
                .Select(l => (object)new
                {
                    l.Id,
                    l.Titulo,
                    l.Autor,
                    l.QuantidadeDisponivel
                })
                .ToList();
        }
    }
}
