namespace Domain.Model;

public class Livro
{
    public int Id { get; set; }
    public string Título { get; set; }
    public string Autor { get; set; }
    public string Descricao { get; set; }
    public string Genero { get; set; }
    public string Editora { get; set; }
    public int QuantidadeDisponivel { get; set; }
}
