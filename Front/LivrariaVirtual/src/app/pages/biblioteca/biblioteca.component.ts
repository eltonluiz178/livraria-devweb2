import { RealizarEmprestimo } from './../../shared/interfaces/realizarEmprestimo';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Livro } from '../../shared/interfaces/livro';
import { LivroServiceService } from '../../shared/services/livroService/livro-service.service';
import { CommonModule } from '@angular/common';
import { CardLivroComponent } from '../../shared/componentes/card-livro/card-livro.component';
import Swal from 'sweetalert2';
import { EmprestimoServiceService } from '../../shared/services/emprestimoService/emprestimo-service.service';
import { Emprestimo } from '../../shared/interfaces/emprestimo';
import { ModalServiceService } from '../../shared/services/modal-service.service';

@Component({
  selector: 'app-biblioteca',
  standalone: true,
  imports: [CommonModule, CardLivroComponent],
  templateUrl: './biblioteca.component.html',
  styleUrl: './biblioteca.component.css'
})
export class BibliotecaComponent implements OnInit,OnChanges {

  constructor(private livroService: LivroServiceService, private emprestimoService: EmprestimoServiceService,private modalService: ModalServiceService) {}

  @Input() livros: Livro[] = [];
  alteracao: { value: number } = { value: 0 };
  estiloSection : string = "height: 83.6dvh;";

  ngOnInit(): void {
    this.exibirLivros();
  }

  ngOnChanges(): void {
    this.exibirLivros();
  }

  exibirLivros(){
    this.livroService.obterLivros().subscribe((response) => {
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

  modalPegarLivro(livro: Livro){
    var emprestimo: RealizarEmprestimo = {
      clienteId : 1,
      livroId : livro.id
    };


    Swal.fire({
      title: 'Confirmar empréstimo',
      text: `Você deseja pegar o livro "${livro.titulo}" emprestado?`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Sim, pegar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.emprestimoService.realizarEmprestimo(emprestimo).subscribe({
          next: (response: any) => {
            this.mudarAlteracao();
            this.exibirLivros();
            this.modalService.sucesso("Sucesso!","Livro pego com sucesso.");
          },
          error: (err) => {
            Swal.fire('Erro!', err.error.errors?.[0] || 'Erro ao realizar empréstimo.', 'error');
          }
        });
      }
    });
  }

  mudarAlteracao(){
    this.alteracao = { value: this.alteracao.value + 1 };
  }
}
