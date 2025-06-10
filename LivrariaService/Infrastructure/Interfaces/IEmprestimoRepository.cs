using Domain.Model;

namespace Infrastructure.Interfaces;

public interface IEmprestimoRepository
{
    Task<List<Emprestimo>> BuscarEmprestimosAsync();
    Task<Emprestimo> BuscarEmprestimoPorIdAsync(int id);
    Task<Emprestimo> BuscarEmprestimoPorLivro(int livroId);
    Task<List<LivroEmprestimo>> BuscarLivroPorCliente(int clienteId);
    Task<Emprestimo> BuscarEmprestimoAtivoPorLivro(int livroId);
    Task<bool> AdicionarEmprestimoAsync(Emprestimo emprestimo);
    Task<bool> EditarEmprestimoAsync(Emprestimo emprestimo);
}
