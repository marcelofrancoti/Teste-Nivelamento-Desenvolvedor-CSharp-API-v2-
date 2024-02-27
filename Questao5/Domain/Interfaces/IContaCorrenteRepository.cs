using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        IEnumerable<ContaCorrente> Get();
        ContaCorrente GetById(string? id);
        void Add(ContaCorrente conta);
        void Update(ContaCorrente conta);
        void Remove(string id);

        decimal GetSaldo(string idContaCorrente);

    }
}
