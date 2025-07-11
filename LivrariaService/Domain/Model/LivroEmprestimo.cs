﻿namespace Domain.Model;

public class LivroEmprestimo
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Descricao { get; set; }
    public string Genero { get; set; }
    public string Editora { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public string Status { get; set; }
}
