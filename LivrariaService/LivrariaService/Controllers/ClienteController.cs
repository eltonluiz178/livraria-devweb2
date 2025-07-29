using Application.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivrariaService.Controllers;

[Route("clientes")]
[ApiController]
[Authorize]
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

    [HttpGet("id-cliente")]
    public async Task<IActionResult> BuscarIdPorUsuario()
    {
        // Pega a claim "unique_name" do token JWT
        var usuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        var id = await _service.BuscarIdPorUsuario(usuario); ;

        return Ok(new { Id = id });
    }
}
