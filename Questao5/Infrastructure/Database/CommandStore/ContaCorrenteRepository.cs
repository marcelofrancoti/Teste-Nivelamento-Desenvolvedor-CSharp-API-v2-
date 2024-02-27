using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Database.CommandStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Microsoft.Data.Sqlite;
    using Questao5.Infrastructure.Sqlite;

    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public ContaCorrente GetById(string id)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
            return connection.QueryFirstOrDefault<ContaCorrente>(query, new { Id = id });
        }

        public void Add(ContaCorrente conta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "INSERT INTO contacorrente (idcontacorrente, numero, nome, ativo) VALUES (@Id, @Numero, @Nome, @Ativo)";
            connection.Execute(query, conta);
        }

        public void Update(ContaCorrente conta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "UPDATE contacorrente SET numero = @Numero, nome = @Nome, ativo = @Ativo WHERE idcontacorrente = @Id";
            connection.Execute(query, conta);
        }

        public void Remove(string id)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "DELETE FROM contacorrente WHERE idcontacorrente = @Id";
            connection.Execute(query, new { Id = id });
        }

        public IEnumerable<ContaCorrente> Get()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "SELECT * FROM contacorrente";
            return connection.Query<ContaCorrente>(query);
        }

        public decimal GetSaldo(string idContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();

            var query = "SELECT SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END) FROM movimento WHERE idcontacorrente = @IdContaCorrente";
            return connection.ExecuteScalar<decimal>(query, new { IdContaCorrente = idContaCorrente });
        }
    }
}
