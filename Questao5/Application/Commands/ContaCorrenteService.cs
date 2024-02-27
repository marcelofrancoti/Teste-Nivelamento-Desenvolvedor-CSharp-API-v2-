using Questao5.Application.Interfaces;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Commands
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public bool ContaExiste(string idContaCorrente)
        {
            var resultado = _contaCorrenteRepository.GetById(idContaCorrente);
            return resultado != null ? true : false;
        }

        public bool ContaAtiva(string idContaCorrente)
        {
            return _contaCorrenteRepository.GetById(idContaCorrente).Ativo;
        }
        public decimal ConsultarSaldo(string idContaCorrente)
        {
            return _contaCorrenteRepository.GetSaldo(idContaCorrente);
        }
    }
}

