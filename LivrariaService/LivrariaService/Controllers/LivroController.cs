using Application.Interfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaService.Controllers;

[Route("livros")]
[ApiController]
[Authorize]
public class LivroController : MainController
{
    private readonly ILivroService _service;
    public LivroController(INotificador notificador, ILivroService service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> BuscarLivrosAsync()
    {
        try
        {
            var livros = await _service.BuscarLivrosAsync();
            return CustomResponse(livros);
        }
        catch(Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarLivroPorIdAsync(int id)
    {
        try
        {
            var livro = await _service.BuscarLivroPorIdAsync(id);
            return CustomResponse(livro);
        }
        catch(Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> AdicionarLivroAsync([FromBody] Livro livro)
    {
        try
        {
            var resposta = await _service.AdicionarLivroAsync(livro);
            return CustomResponse(resposta);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPut("")]
    public async Task<IActionResult> EditarLivroAsync([FromBody] Livro livro)
    {
        try
        {
            var resposta = await _service.EditarLivroAsync(livro);
            return CustomResponse(resposta);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarLivroAsync(int id)
    {
        try
        {
            var resposta = await _service.DeletarLivroAsync(id);
            return CustomResponse(resposta);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }
}
