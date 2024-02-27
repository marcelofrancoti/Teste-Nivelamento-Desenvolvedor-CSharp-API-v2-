using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentoService _movimentoService;
        private readonly IContaCorrenteService _contaCorrenteService;

        public MovimentacaoController(IMovimentoService movimentoService, IContaCorrenteService contaCorrenteService)
        {
            _movimentoService = movimentoService;
            _contaCorrenteService = contaCorrenteService;
        }

        [HttpPost]
        [Route("movimentacao")]
        public IActionResult MovimentacaoContaCorrente([FromBody] Movimento request)
        {
            try
            {
                var contaCorrenteExistente = _contaCorrenteService.ContaExiste(request.IdContaCorrente);
                if (!contaCorrenteExistente)
                {
                    return StatusCode(400, "Conta corrente não encontrada.");
                }

                var contaCorrenteAtiva = _contaCorrenteService.ContaAtiva(request.IdContaCorrente);
                if (!contaCorrenteAtiva)
                {
                    return StatusCode(400, "Conta corrente inativa.");
                }

                string chaveIdempotencia = Guid.NewGuid().ToString();

                _movimentoService.CriarMovimento(request, chaveIdempotencia);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao realizar a movimentação da conta corrente: {ex.Message}");
            }
        }

    }
}
