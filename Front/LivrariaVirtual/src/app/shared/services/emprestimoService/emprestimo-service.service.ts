import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RealizarEmprestimo } from '../../interfaces/realizarEmprestimo';

@Injectable({
  providedIn: 'root'
})
export class EmprestimoServiceService {
  private readonly API = 'https://localhost:7173/emprestimos'

  constructor(private http: HttpClient) { }

  obterEmprestimoPorLivro(id: number): Observable<any>{
    const url = `${this.API}/livro/${id}`
    return this.http.get<any>(url);
  }

  obterEmprestimoPorCliente(id: number): Observable<any>{
    const url = `${this.API}/cliente/${id}`
    return this.http.get<any>(url);
  }

  estenderEmprestimo(id: number): Observable<any>{
    const url = `${this.API}/estender/${id}`
    return this.http.put<any>(url,null);
  }

  desativarEmprestimo(id: number): Observable<any>{
    const url = `${this.API}/desativar/${id}`
    return this.http.put<any>(url,null);
  }

  realizarEmprestimo(emprestimo: RealizarEmprestimo): Observable<any>{
    return this.http.post(this.API,emprestimo);
  }
}
