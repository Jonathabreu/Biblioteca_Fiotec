namespace Biblioteca.Application.Interfaces
{
    public interface IUnityOfWork
    {
        Task ExecuteEmTransacaoAsync(Func<Task> acao);
    }
}
