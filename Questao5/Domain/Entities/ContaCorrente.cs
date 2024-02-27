namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string Id { get; set; } // Id da conta corrente
        public int Numero { get; set; } // Número da conta corrente
        public string NomeTitular { get; set; } // Nome do titular da conta corrente
        public bool Ativo { get; set; } // Indica se a conta está ativa (true = ativa, false = inativa)
    }
}
