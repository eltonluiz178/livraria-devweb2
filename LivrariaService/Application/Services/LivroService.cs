using Application.Interfaces;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Application.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _repository;

    public LivroService(ILivroRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> AdicionarLivroAsync(Livro livro)
    {
        try
        {
            var resposta = await _repository.AdicionarLivroAsync(livro);

            if(resposta == false)
            {
                throw new Exception("Erro ao adicionar o livro, verifique os campos e tente novamente.");
            }

            return resposta;
        }
        catch (Exception e)
        { throw e; }
    }

    public async Task<Livro> BuscarLivroPorIdAsync(int id)
    {
        try
        {
            var livro = await _repository.BuscarLivroPorIdAsync(id);

            if(livro == null)
            {
                throw new Exception($"Não foi possível localizar o livro com id {id}.");
            }
            return livro;
        }
        catch (Exception e)
        { throw e; }
    }

    public async Task<List<Livro>> BuscarLivrosAsync()
    {
        try
        {
            var livros = await _repository.BuscarLivrosAsync();

            if (livros == null)
            {
                throw new Exception($"Não foi possível localizar nenhum livro.");
            }
            return livros;
        }
        catch (Exception e)
        { throw e; }
    }

    public async Task<bool> DeletarLivroAsync(int id)
    {
        try
        {
            var resposta = await _repository.DeletarLivroAsync(id);

            if (resposta == false)
            {
                throw new Exception("Erro ao deletar o livro.");
            }

            return resposta;
        }
        catch (Exception e)
        { throw e; }
    }

    public async Task<bool> EditarLivroAsync(Livro livro)
    {
        try
        {
            if (await _repository.BuscarLivroPorIdAsync(livro.Id) == null)
            {
                throw new Exception($"Não foi possível localizar o livro com id {livro.Id}.");
            }

            var resposta = await _repository.EditarLivroAsync(livro);

            if (resposta == false)
            {
                throw new Exception("Erro ao editar o livro, verifique os campos e tente novamente.");
            }

            return resposta;
        }
        catch (Exception e)
        { throw e; }
    }
}