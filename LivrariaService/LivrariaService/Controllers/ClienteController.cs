using Application.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaService.Controllers;

[Route("clientes")]
[ApiController]
public class ClienteController : MainController
{
    private readonly IClienteService _service;

    public ClienteController(INotificador notificador, IClienteService service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarClientePorIdAsync(int id)
    {
        try
        {
            var cliente = await _service.BuscarClientePorIdAsync(id);
            return CustomResponse(cliente);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> AdicionarClienteAsync ([FromBody] Cliente cliente)
    {
        try
        {
            var resposta = await _service.AdicionarClienteAsync(cliente);
            return CustomResponse(resposta);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }
}
