using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Commands
{
    public class MovimentoService : IMovimentoService
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public MovimentoService(IMovimentoRepository movimentoRepository, IIdempotenciaRepository idempotenciaRepository)
        {
            _movimentoRepository = movimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public void CriarMovimento(Movimento movimento, string chaveIdempotencia)
        {
            if (_idempotenciaRepository.ChaveExiste(chaveIdempotencia))
            {
                // A chave de idempotência já foi processada, retornar o resultado anterior
                var resultadoAnterior = _idempotenciaRepository.ObterResultado(chaveIdempotencia);
                // Você pode retornar o resultado anterior ou lançar uma exceção informando que a ação já foi processada
                // Neste exemplo, estamos lançando uma exceção
                throw new Exception("Esta ação já foi processada anteriormente.");
            }

            // Executar a ação correspondente (no caso, criar um novo movimento)
            _movimentoRepository.Add(movimento);

            // Registrar o resultado associado à chave de idempotência
            _idempotenciaRepository.RegistrarResultado(chaveIdempotencia, "Movimento criado com sucesso.");
        }

    }

}
