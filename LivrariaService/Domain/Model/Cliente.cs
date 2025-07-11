﻿namespace Domain.Model;

public class Cliente
{
    public int Id { get; set; }
    public string CPF { get; set; }
    public string Nome { get; set; }
    public string NomeUsuario { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
}
