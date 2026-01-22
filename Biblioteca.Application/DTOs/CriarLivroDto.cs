namespace Biblioteca.Application.DTOs
{
    public record CriarLivroDto(
        string Titulo,
        string Autor,
        int QuantidadeInicial
    );
}
