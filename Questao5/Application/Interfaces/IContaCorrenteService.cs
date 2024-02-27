namespace Questao5.Application.Interfaces
{
    public interface IContaCorrenteService
    {
        bool ContaExiste(string id);
        bool ContaAtiva(string id);
        decimal ConsultarSaldo(string idContaCorrente);
    }
}
