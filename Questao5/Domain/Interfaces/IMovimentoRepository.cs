using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Movimento GetById(string id);
        void Add(Movimento movimento);

    }
}
