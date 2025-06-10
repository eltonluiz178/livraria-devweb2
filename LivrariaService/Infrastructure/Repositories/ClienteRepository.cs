using System.Data;
using Dapper;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IDbConnection _connection;

    public ClienteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> AdicionarClienteAsync(Cliente cliente)
    {
        try
        {
            var sql = "INSERT INTO Cliente VALUES (@CPF,@NOME,@NOMEUSUARIO,@SENHA,@EMAIL,@TELEFONE,@DATANASCIMENTO,@ENDERECO)";
            var parametros = new
            {
                CPF = cliente.CPF,
                NOME = cliente.Nome,
                NOMEUSUARIO = cliente.NomeUsuario,
                SENHA = cliente.Senha,
                EMAIL = cliente.Email,
                TELEFONE = cliente.Telefone,
                DATANASCIMENTO = cliente.DataNascimento,
                ENDERECO = cliente.Endereco
            };

            var resposta = await _connection.ExecuteAsync(sql, parametros);

            return resposta > 0 ? true : false;
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao adicionar o cliente.");
        }
    }

    public async Task<Cliente> BuscarClientePorIdAsync(int id)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM Cliente WHERE Id = {id}";

            var cliente = await _connection.QueryFirstOrDefaultAsync<Cliente>(sql);

            return cliente;
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao buscar o cliente.");
        }
    }
}
