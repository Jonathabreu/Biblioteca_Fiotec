namespace Biblioteca.Application.DTOs
{
    public record CriarEmprestimoDto(
        Guid LivroId,
        string NomeSolicitante
    );
}
