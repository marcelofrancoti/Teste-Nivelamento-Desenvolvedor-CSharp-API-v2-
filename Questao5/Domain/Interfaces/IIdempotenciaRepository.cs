namespace Questao5.Domain.Interfaces
{
    public interface IIdempotenciaRepository
    {
        bool ChaveExiste(string chaveIdempotencia);
        void RegistrarResultado(string chaveIdempotencia, string resultado);
        string ObterResultado(string chaveIdempotencia);
    }
}
