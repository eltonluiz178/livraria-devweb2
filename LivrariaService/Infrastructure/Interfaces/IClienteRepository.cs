using Domain.Model;

namespace Infrastructure.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> BuscarClientePorIdAsync (int id);
    Task<bool> AdicionarClienteAsync (Cliente cliente);
    Task<int> BuscarIdPorUsuario(string NomeUsuario);
 }
