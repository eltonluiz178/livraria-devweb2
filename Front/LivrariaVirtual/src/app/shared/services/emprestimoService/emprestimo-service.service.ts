import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RealizarEmprestimo } from '../../interfaces/realizarEmprestimo';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmprestimoServiceService {
  private url: string = environment.apiUrl + "/emprestimos"

  constructor(private http: HttpClient) {
    console.log('API URL:', this.url);
   }

  obterEmprestimoPorLivro(id: number): Observable<any>{
    const url = `${this.url}/livro/${id}`
    return this.http.get<any>(url);
  }

  obterEmprestimoPorCliente(id: number): Observable<any>{
    const url = `${this.url}/cliente/${id}`
    return this.http.get<any>(url);
  }

  estenderEmprestimo(id: number): Observable<any>{
    const url = `${this.url}/estender/${id}`
    return this.http.put<any>(url,null);
  }

  desativarEmprestimo(id: number): Observable<any>{
    const url = `${this.url}/desativar/${id}`
    return this.http.put<any>(url,null);
  }

  realizarEmprestimo(emprestimo: RealizarEmprestimo): Observable<any>{
    return this.http.post(this.url,emprestimo);
  }
}
