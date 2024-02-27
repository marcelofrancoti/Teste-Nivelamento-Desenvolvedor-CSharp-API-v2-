namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string Id { get; set; } // Id do movimento
        public string IdContaCorrente { get; set; } // Id da conta corrente associada ao movimento
        public DateTime DataMovimento { get; set; } // Data do movimento
        public char TipoMovimento { get; set; } // Tipo do movimento ('C' para Crédito, 'D' para Débito)
        public decimal Valor { get; set; } // Valor do movimento
    }
}
