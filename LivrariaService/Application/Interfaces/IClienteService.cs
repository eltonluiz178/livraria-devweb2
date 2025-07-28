using Domain.Model;

namespace Application.Interfaces;

public interface IClienteService
{
    Task<Cliente> BuscarClientePorIdAsync(int id);
    Task<bool> AdicionarClienteAsync(Cliente cliente);
    Task<int> BuscarIdPorUsuario(string NomeUsuario);
}
