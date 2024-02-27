using Questao5.Domain.Entities;

namespace Questao5.Application.Interfaces
{
    public interface IMovimentoService
    {
        void CriarMovimento(Movimento movimento, string chaveIdempotencia);
    }
}
