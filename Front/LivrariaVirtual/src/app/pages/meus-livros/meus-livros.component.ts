import { Emprestimo } from './../../shared/interfaces/emprestimo';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { CardLivroComponent } from "../../shared/componentes/card-livro/card-livro.component";
import { EmprestimoServiceService } from '../../shared/services/emprestimoService/emprestimo-service.service';
import { LivroEmprestimo } from '../../shared/interfaces/livroEmprestimo';
import { Livro } from '../../shared/interfaces/livro';
import { ModalServiceService } from '../../shared/services/modal-service.service';

@Component({
  selector: 'app-meus-livros',
  standalone: true,
  imports: [CardLivroComponent],
  templateUrl: './meus-livros.component.html',
  styleUrl: './meus-livros.component.css'
})
export class MeusLivrosComponent implements OnInit,OnChanges {

  constructor(private emprestimoService: EmprestimoServiceService, private modalService: ModalServiceService) {}

  @Input() livros: LivroEmprestimo[] = [];
  estiloSection : string = "height: 83.6dvh;";

  ngOnInit(): void {
    this.exibirLivros();
  }

  ngOnChanges(): void {
    this.exibirLivros();
  }

  exibirLivros(){
    this.emprestimoService.obterEmprestimoPorCliente(1).subscribe((response) => {
      this.livros = response.data;
      this.formatarSection();
    });
  }

  formatarSection(){
    if(this.livros.length <= 3 || this.livros == null)
    {
      this.estiloSection = "height: 83.6dvh;"
    }

    else {
      this.estiloSection = "height: auto;"
    }
  }

  estenderEmprestimo(livro : Livro){
    this.emprestimoService.obterEmprestimoPorLivro(livro.id).subscribe((response) => {
      const emprestimo: Emprestimo = response.data;
      this.emprestimoService.estenderEmprestimo(emprestimo.id).subscribe({
        next: (response: any) => {
          this.exibirLivros()
          this.modalService.sucesso("Sucesso!","O empréstimo foi estendido com sucesso.");
        },
        error: (err) => {
          this.modalService.erro("Erro!",err.error.errors[0])
        }
      })
    })
  }

  devolverLivro(livro : Livro){
    this.emprestimoService.obterEmprestimoPorLivro(livro.id).subscribe((response) => {
      const emprestimo: Emprestimo = response.data;
      this.emprestimoService.desativarEmprestimo(emprestimo.id).subscribe({
        next: (response: any) => {
          this.exibirLivros()
          this.modalService.sucesso("Sucesso!","O livro voi devolvido com sucesso.")
        },
        error: (err) => {
          this.modalService.erro("Erro!",err.error.errors[0])
        }
      })
    })
  }
}
