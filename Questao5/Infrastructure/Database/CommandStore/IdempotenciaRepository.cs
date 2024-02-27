using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{

    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotenciaRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public bool ChaveExiste(string chaveIdempotencia)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            connection.Open();

            var query = "SELECT 1 FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            return connection.ExecuteScalar<int?>(query, new { ChaveIdempotencia = chaveIdempotencia }) != null;
        }

        public void RegistrarResultado(string chaveIdempotencia, string resultado)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            connection.Open();

            var query = "INSERT INTO idempotencia (chave_idempotencia, resultado) VALUES (@ChaveIdempotencia, @Resultado)";
            connection.Execute(query, new { ChaveIdempotencia = chaveIdempotencia, Resultado = resultado });
        }

        public string ObterResultado(string chaveIdempotencia)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            connection.Open();

            var query = "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            return connection.ExecuteScalar<string>(query, new { ChaveIdempotencia = chaveIdempotencia });
        }
    }
}
