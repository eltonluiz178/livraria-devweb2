using Domain.Model;

namespace Infrastructure.Interfaces;

public interface ILivroRepository
{
    Task<List<Livro>> BuscarLivrosAsync();
    Task<Livro> BuscarLivroPorIdAsync(int id);
    Task<bool> AdicionarLivroAsync(Livro livro);
    Task<bool> EditarLivroAsync(Livro livro);
    Task<bool> DeletarLivroAsync(int id);
}
