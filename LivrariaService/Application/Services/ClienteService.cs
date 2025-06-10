using Application.Interfaces;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> AdicionarClienteAsync(Cliente cliente)
    {
        try
        {
            var resposta = await _repository.AdicionarClienteAsync(cliente);

            if(resposta == false)
            {
                throw new Exception("Não foi possível adicionar o cliente, verifique os campos e tente novamente.");
            }
            return resposta;
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    public async Task<Cliente> BuscarClientePorIdAsync(int id)
    {
        try
        {
            var cliente = await _repository.BuscarClientePorIdAsync(id);

            if(cliente == null)
            {
                throw new Exception($"Não possível encontra o cliente com id {id}.");
            }
            return cliente;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
}
