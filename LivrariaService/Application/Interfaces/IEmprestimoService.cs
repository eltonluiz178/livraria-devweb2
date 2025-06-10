using Domain.Dtos;
using Domain.Model;

namespace Application.Interfaces;

public interface IEmprestimoService
{
    Task<List<Emprestimo>> BuscarEmprestimosAsync();
    Task<Emprestimo> BuscarEmprestimoPorIdAsync(int id);
    Task<Emprestimo> BuscarEmprestimoPorLivroAsync(int livroId);
    Task<List<LivroEmprestimo>> BuscarLivroPorClienteAsync(int clienteId);
    Task<Emprestimo> BuscarEmprestimoAtivoPorLivroAsync(int livroId);
    Task<bool> AdicionarEmprestimoAsync(CriarEmprestimoDto emprestimoDto);
    Task<bool> EstenderEmprestimoAsync(int id);
    Task<Emprestimo> DesativarEmprestimoAsync(int id);
}
