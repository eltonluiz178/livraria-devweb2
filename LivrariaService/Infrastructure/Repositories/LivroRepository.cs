using System.Data;
using Dapper;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly IDbConnection _connection;

    public LivroRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> AdicionarLivroAsync(Livro livro)
    {
        try
        {
            var sql = "INSERT INTO Livro VALUES (@TITULO, @AUTOR, @DESCRICAO, @GENERO, @EDITORA, @QUANTIDADE)";
            var parametros = new
            {
                TITULO = livro.Título,
                AUTOR = livro.Autor,
                DESCRICAO = livro.Descricao,
                GENERO = livro.Genero,
                EDITORA = livro.Editora,
                QUANTIDADE = livro.QuantidadeDisponivel
            };

            var resposta = await _connection.ExecuteAsync(sql, parametros);

            return resposta > 0 ? true : false;
        }
        catch
        (Exception e)
        {
            throw new Exception("Houve um erro ao adicionar o livro.");
        }
    }

    public async Task<Livro> BuscarLivroPorIdAsync(int id)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM Livro WHERE Id = {id}";

            var livro = await _connection.QueryFirstOrDefaultAsync<Livro>(sql);

            return livro;
        }
        catch
        (Exception e)
        {
            throw new Exception("Houve um erro ao buscar o livro.");
        }
    }

    public async Task<List<Livro>> BuscarLivrosAsync()
    {
        try
        {
            var sql = "SELECT * FROM Livro";

            var livros = await _connection.QueryAsync<Livro>(sql);

            return livros.ToList();
        }
        catch
        (Exception e)
        {
            throw new Exception("Houve um erro ao buscar os livros.");
        }
    }

    public async Task<bool> DeletarLivroAsync(int id)
    {
        try
        {
            var sql = $"DELETE FROM Livro WHERE Id = {id}";

            var resposta = await _connection.ExecuteAsync(sql);

            return resposta > 0 ? true : false;
        }
        catch
        (Exception e)
        {
            throw new Exception("Houve um erro ao deletar o livro.");
        }
    }

    public async Task<bool> EditarLivroAsync(Livro livro)
    {
        try
        {
            var sql = "UPDATE Livro SET Titulo=@TITULO,Autor=@AUTOR,Descricao=@DESCRICAO,Genero=@GENERO,Editora=@EDITORA,QuantidadeDisponivel=@QUANTIDADE WHERE Id=@ID";

            var parametros = new
            {
                TITULO = livro.Título,
                AUTOR = livro.Autor,
                DESCRICAO = livro.Descricao,
                GENERO = livro.Genero,
                EDITORA = livro.Editora,
                QUANTIDADE = livro.QuantidadeDisponivel,
                ID = livro.Id
            };

            var resposta = await _connection.ExecuteAsync(sql, parametros);

            return resposta > 0 ? true : false;
        }
        catch
        (Exception e)
        {
            throw new Exception("Houve um erro ao editar o livro.");
        }
    }
}
