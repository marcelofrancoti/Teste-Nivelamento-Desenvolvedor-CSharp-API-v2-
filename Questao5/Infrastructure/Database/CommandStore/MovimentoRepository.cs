using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Movimento GetById(string id)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "SELECT * FROM movimento WHERE idmovimento = @Id";
            return connection.QueryFirstOrDefault<Movimento>(query, new { Id = id });
        }

        public void Add(Movimento movimento)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                        "VALUES (@Id, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";
            connection.Execute(query, movimento);
        }

        // Implemente os métodos restantes conforme necessário
    }
}
