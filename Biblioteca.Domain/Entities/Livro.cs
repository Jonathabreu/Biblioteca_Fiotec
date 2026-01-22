namespace Biblioteca.Dominio.Entidades;

public class Livro
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Titulo { get; private set; } = "";
    public string Autor { get; private set; } = "";
    public int QuantidadeDisponivel { get; private set; }

    private Livro() { }

    public Livro(string titulo, string autor, int quantidadeInicial)
    {
        AtualizarDados(titulo, autor);
        DefinirQuantidadeInicial(quantidadeInicial);
    }

    public void AtualizarDados(string titulo, string autor)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.");
        if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("Autor é obrigatório.");

        Titulo = titulo.Trim();
        Autor = autor.Trim();
    }

    private void DefinirQuantidadeInicial(int quantidadeInicial)
    {
        if (quantidadeInicial <= 0)
            throw new ArgumentException("Não permitir cadastro de livros com quantidade inicial menor ou igual a zero.");

        QuantidadeDisponivel = quantidadeInicial;
    }

    public void BaixarEstoque()
    {
        if (QuantidadeDisponivel <= 0)
            throw new InvalidOperationException("Não há exemplares disponíveis para empréstimo.");

        QuantidadeDisponivel--;
    }

    public void DevolverAoEstoque()
    {
        QuantidadeDisponivel++;
    }
}
