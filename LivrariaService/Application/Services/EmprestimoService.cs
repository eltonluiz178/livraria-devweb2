using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Model;
using Infrastructure.Interfaces;

namespace Application.Services;

public class EmprestimoService : IEmprestimoService
{
    private readonly IEmprestimoRepository _repository;
    private readonly IMapper _mapper;

    public EmprestimoService(IEmprestimoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> AdicionarEmprestimoAsync(CriarEmprestimoDto emprestimoDto)
    {
        try
        {
            var emprestimo = _mapper.Map<Emprestimo>(emprestimoDto);
            emprestimo.DataEmprestimo = DateTime.Now;
            emprestimo.DataDevolucaoPrevista = DateTime.Now.AddDays(20);
            emprestimo.DataDevolucaoReal = DateTime.Now.AddDays(20);
            emprestimo.Multa = 0;
            emprestimo.Status = "ativo";

            var emprestimoPorLivro = await _repository.BuscarEmprestimoPorLivro(emprestimo.LivroId);
            if (emprestimoPorLivro != null && emprestimoPorLivro.Status == "ativo")
            {
                throw new Exception("O livro não está disponível para emprestimo.");
            }

            var resposta = await _repository.AdicionarEmprestimoAsync(emprestimo);

            if (resposta == false)
            {
                throw new Exception("Não foi possível adicionar o empréstimo, verifique os campos e tente novamente.");
            }

            return resposta;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<Emprestimo> BuscarEmprestimoPorIdAsync(int id)
    {
        try
        {
            var emprestimo = await _repository.BuscarEmprestimoPorIdAsync(id);

            if (emprestimo == null)
            {
                throw new Exception($"Não foi possível localizar nenhum empréstimo com id {id}");
            }

            return emprestimo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<Emprestimo> BuscarEmprestimoPorLivroAsync(int livroId)
    {
        try
        {
            var emprestimo = await _repository.BuscarEmprestimoPorLivro(livroId);

            if (emprestimo == null)
            {
                throw new Exception($"Não foi possível localizar nenhum empréstimo com id do livro {livroId}");
            }

            return emprestimo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }    public async Task<List<LivroEmprestimo>> BuscarLivroPorClienteAsync(int clienteId)
    {
        try
        {
            var livros = await _repository.BuscarLivroPorCliente(clienteId);

            return livros;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    public async Task<Emprestimo> BuscarEmprestimoAtivoPorLivroAsync(int livroId)
    {
        try
        {
            var emprestimo = await _repository.BuscarEmprestimoAtivoPorLivro(livroId);

            return emprestimo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<List<Emprestimo>> BuscarEmprestimosAsync()
    {
        try
        {
            var emprestimos = await _repository.BuscarEmprestimosAsync();

            if (emprestimos == null)
            {
                throw new Exception($"Não foi possível localizar nenhum empréstimo");
            }

            return emprestimos;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<Emprestimo> DesativarEmprestimoAsync(int id)
    {
        try
        {
            var emprestimo = await _repository.BuscarEmprestimoPorIdAsync(id);

            if (emprestimo == null)
            {
                throw new Exception($"Não foi possível localizar emprestimo com id {id}");
            }

            emprestimo.DataDevolucaoReal = DateTime.Now;
            
            if (emprestimo.DataDevolucaoReal > emprestimo.DataDevolucaoPrevista)
            {
                int diasDeDiferenca = Math.Abs((emprestimo.DataDevolucaoReal - emprestimo.DataDevolucaoPrevista).Days);

                emprestimo.Multa = 15 + (diasDeDiferenca * 2);
            }

            emprestimo.Status = "inativo";

            var resposta = await _repository.EditarEmprestimoAsync(emprestimo);

            if (resposta == false)
            {
                throw new Exception("Houve um erro ao desativar o emprestimo");
            }

            return emprestimo;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<bool> EstenderEmprestimoAsync(int id)
    {
        try
        {
            var emprestimo = await _repository.BuscarEmprestimoPorIdAsync(id);

            if (emprestimo == null)
            {
                throw new Exception($"Não foi possível localizar emprestimo com id {id}");
            }

            if (emprestimo.Status == "inativo")
            {
                throw new Exception("Não é possível estender um emprestimo inativo é necessário fazer um novo empréstimo");
            }

            if(DateTime.Now > emprestimo.DataDevolucaoPrevista)
            {
                throw new Exception("Não é possível estender um emprestimo vencido");
            }

            if((emprestimo.DataDevolucaoPrevista - DateTime.Now).Days > 20)
            {
                throw new Exception("Não é possível estendar a data tendo mais que 20 dias.");
            }


            emprestimo.DataDevolucaoPrevista = emprestimo.DataDevolucaoPrevista.AddDays(20);

            var resposta = await _repository.EditarEmprestimoAsync(emprestimo);

            if (resposta == false)
            {
                throw new Exception("Houve um erro ao estender a data do emprestimo");
            }

            return resposta;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
