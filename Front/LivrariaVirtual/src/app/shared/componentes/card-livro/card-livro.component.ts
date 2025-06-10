import { EmprestimoServiceService } from './../../services/emprestimoService/emprestimo-service.service';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Livro } from '../../interfaces/livro';
import { LivroEmprestimo } from '../../interfaces/livroEmprestimo';
import { RealizarEmprestimo } from '../../interfaces/realizarEmprestimo';

@Component({
  selector: 'app-card-livro',
  standalone: true,
  imports: [],
  templateUrl: './card-livro.component.html',
  styleUrl: './card-livro.component.css'
})
export class CardLivroComponent implements OnInit, OnChanges {
  @Input() livro: Livro | null = null;
  @Input() estiloCard: string = "";
  @Input() situacao: boolean = false;
  @Input() telaEmprestimo: boolean = false;
  @Input() dataEmprestimo: Date | string = "";
  @Input() alteracao: { value: number } = { value: 0 };

  validacaoData: number = 0;

  @Output() estender = new EventEmitter<Livro>();
  @Output() devolver = new EventEmitter<Livro>();
  @Output() pegar = new EventEmitter<Livro>();


  constructor(private emprestimoService: EmprestimoServiceService, private cdr: ChangeDetectorRef) { }

  @Input() livroEmprestimo: LivroEmprestimo | null = null;

  ngOnInit(): void {
    this.carregarEmprestimo();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['alteracao'] || changes['livro']) {
      this.carregarEmprestimo();
    }
  }

  carregarEmprestimo() {
    if (this.livro) {
      this.emprestimoService.obterEmprestimoPorLivro(this.livro.id).subscribe((response) => {
        this.livroEmprestimo = response.data;
        this.situacao = this.livroEmprestimo == null; // Atualiza situacao
        this.verificaDias();
        this.cdr.detectChanges(); // Forçar detecção de mudanças
      });
    }
  }

  verificaDias() {
    if (this.livroEmprestimo?.dataDevolucaoPrevista) {
      const dataDevolucao = new Date(this.livroEmprestimo.dataDevolucaoPrevista);
      const hoje = new Date(Date.now());

      // Zera horas para comparar apenas datas
      dataDevolucao.setHours(0, 0, 0, 0);
      hoje.setHours(0, 0, 0, 0);

      const diffMs = dataDevolucao.getTime() - hoje.getTime();
      const diffDias = diffMs / (1000 * 60 * 60 * 24);

      if (diffDias <= 0) {
        this.validacaoData = 1
      }
      else if (diffDias > 0 && diffDias <= 5) {
        this.validacaoData = 2;
      }

    }
  }

  formatarData(data: string | Date): string {
    const dataConvertida = typeof data === 'string' ? new Date(data) : data;
    const dia: string = dataConvertida.getDate().toString().padStart(2, '0');
    const mes: string = (dataConvertida.getMonth() + 1).toString().padStart(2, '0');
    const ano: number = dataConvertida.getFullYear();
    return `${dia}/${mes}/${ano}`;
  }

  estenderEmprestimo() {
    if (this.livro) {
      this.estender.emit(this.livro);
    }
  }

  devolverLivro(){
    if (this.livro) {
      this.devolver.emit(this.livro);
    }
  }

  pegarLivro(){
    if (this.livro) {
      this.pegar.emit(this.livro);
    }
  }
}
