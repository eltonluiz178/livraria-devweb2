using System.Data;
using Dapper;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly IDbConnection _connection;

    public EmprestimoRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> AdicionarEmprestimoAsync(Emprestimo emprestimo)
    {
        try
        {
            var sql = "INSERT INTO Emprestimo VALUES (@DATAEMPRESTIMO,@DATAPREVISTA,@DATAREAL,@MULTA,@STATUS,@CLIENTEID,@LIVROID)";
            var parametros = new
            {
                DATAEMPRESTIMO = emprestimo.DataEmprestimo,
                DATAPREVISTA = emprestimo.DataDevolucaoPrevista,
                DATAREAL = emprestimo.DataDevolucaoReal,
                MULTA = emprestimo.Multa,
                STATUS = emprestimo.Status,
                CLIENTEID = emprestimo.ClienteId,
                LIVROID = emprestimo.LivroId
            };

            var resposta = await _connection.ExecuteAsync(sql, parametros);

            return resposta > 0 ? true : false;
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao adicionar o emprestimo");
        }
    }

    public async Task<Emprestimo> BuscarEmprestimoPorLivro(int livroId)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM Emprestimo WHERE LivroId={livroId}";

            var emprestimo = await _connection.QueryFirstOrDefaultAsync<Emprestimo>(sql);

            return emprestimo;
        }
        catch(Exception e)
        {
            throw new Exception("Houve um erro ao buscar o emprestimo");
        }
    }
    
    public async Task<List<LivroEmprestimo>> BuscarLivroPorCliente(int clienteId)
    {
        try
        {
            var sql = $"SELECT L.Id,L.Titulo,L.Autor,L.Descricao,L.Genero,L.Editora,E.DataDevolucaoPrevista,E.Status FROM Livro L INNER JOIN Emprestimo E ON L.Id = E.LivroId WHERE E.ClienteId={clienteId} AND E.Status='ativo'";

            var livros = await _connection.QueryAsync<LivroEmprestimo>(sql);

            return livros.ToList();
        }
        catch(Exception e)
        {
            throw new Exception("Houve um erro ao buscar o emprestimo");
        }
    }
    
    public async Task<Emprestimo> BuscarEmprestimoAtivoPorLivro(int livroId)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM Emprestimo WHERE LivroId={livroId} AND Status='ativo'";

            var emprestimo = await _connection.QueryFirstOrDefaultAsync<Emprestimo>(sql);

            return emprestimo;
        }
        catch(Exception e)
        {
            throw new Exception("Houve um erro ao buscar o emprestimo");
        }
    }

    public async Task<Emprestimo> BuscarEmprestimoPorIdAsync(int id)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM Emprestimo WHERE Id={id}";

            var emprestimo = await _connection.QueryFirstOrDefaultAsync<Emprestimo>(sql);

            return emprestimo;
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao buscar o emprestimo");
        }
    }

    public async Task<List<Emprestimo>> BuscarEmprestimosAsync()
    {
        try
        {
            var sql = "SELECT * FROM Emprestimo";

            var emprestimos = await _connection.QueryAsync<Emprestimo>(sql);

            return emprestimos.ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao buscar os emprestimos");
        }
    }

    public async Task<bool> EditarEmprestimoAsync(Emprestimo emprestimo)
    {
        try
        {
            var sql = "UPDATE Emprestimo SET DataEmprestimo=@DATAEMPRESTIMO,DataDevolucaoPrevista=@DATAPREVISTA,DataDevolucaoReal=@DATAREAL,Multa=@MULTA,Status=@STATUS,ClienteId=@CLIENTEID,LivroId=@LIVROID WHERE Id=@ID";
            var parametros = new
            {
                DATAEMPRESTIMO = emprestimo.DataEmprestimo,
                DATAPREVISTA = emprestimo.DataDevolucaoPrevista,
                DATAREAL = emprestimo.DataDevolucaoReal,
                MULTA = emprestimo.Multa,
                STATUS = emprestimo.Status,
                CLIENTEID = emprestimo.ClienteId,
                LIVROID = emprestimo.LivroId,
                ID = emprestimo.Id
            };

            var resposta = await _connection.ExecuteAsync(sql, parametros);

            return resposta > 0 ? true : false;
        }
        catch (Exception e)
        {
            throw new Exception("Houve um erro ao editar o emprestimo");
        }
    }
}
