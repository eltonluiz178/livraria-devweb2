namespace Domain.Model;

public class Emprestimo
{
    public int Id { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public DateTime DataDevolucaoReal { get; set; }
    public decimal Multa { get; set; }
    public string Status { get; set; }
    public int ClienteId { get; set; }
    public int LivroId { get; set; }
}
