using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaService.Controllers;

[Route("emprestimos")]
[ApiController]
public class EmprestimoController : MainController
{
    private readonly IEmprestimoService _service;

    public EmprestimoController(INotificador notificador, IEmprestimoService service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> BuscarEmprestimosAsync()
    {
        try
        {
            var emprestimos = await _service.BuscarEmprestimosAsync();
            return CustomResponse(emprestimos);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarEmprestimoPorIdAsync(int id)
    {
        try
        {
            var emprestimo = await _service.BuscarEmprestimoPorIdAsync(id);
            return CustomResponse(emprestimo);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("livro/{id}")]
    public async Task<IActionResult> BuscarEmprestimoAtivoPorLivroAsync(int id)
    {
        try
        {
            var emprestimo = await _service.BuscarEmprestimoAtivoPorLivroAsync(id);
            return CustomResponse(emprestimo);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> BuscarLivroPorClienteAsync(int id)
    {
        try
        {
            var livros = await _service.BuscarLivroPorClienteAsync(id);
            return CustomResponse(livros);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> AdicionarEmprestimoAsync([FromBody]CriarEmprestimoDto emprestimoDto)
    {
        try
        {
            var emprestimo = await _service.AdicionarEmprestimoAsync(emprestimoDto);
            return CustomResponse(emprestimo);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPut("estender/{id}")]
    public async Task<IActionResult> EstenderEmprestimoAsync(int id)
    {
        try
        {
            var resposta = await _service.EstenderEmprestimoAsync(id);
            return CustomResponse(resposta);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPut("desativar/{id}")]
    public async Task<IActionResult> DesativarEmprestimoAsync(int id)
    {
        try
        {
            var emprestimo = await _service.DesativarEmprestimoAsync(id);
            return CustomResponse(emprestimo);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }
}
