using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Interfaces;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoController : ControllerBase
    {
        private readonly IContaCorrenteService _contaCorrenteService;

        public SaldoController(IContaCorrenteService contaCorrenteService)
        {
            _contaCorrenteService = contaCorrenteService;
        }

        [HttpGet]
        [Route("saldo/{idContaCorrente}")]
        public IActionResult SaldoContaCorrente(string idContaCorrente)
        {
            try
            {
                var contaCorrenteExistente = _contaCorrenteService.ContaExiste(idContaCorrente);
                if (!contaCorrenteExistente)
                {
                    return StatusCode(400, "Conta corrente não encontrada.");
                }

                var contaCorrenteAtiva = _contaCorrenteService.ContaAtiva(idContaCorrente);
                if (!contaCorrenteAtiva)
                {
                    return StatusCode(400, "Conta corrente inativa.");
                }

                var saldo = _contaCorrenteService.ConsultarSaldo(idContaCorrente);
                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao consultar o saldo da conta corrente: {ex.Message}");
            }
        }
    }
}
