import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RealizarEmprestimo } from '../../interfaces/realizarEmprestimo';
import { environment } from '../../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class EmprestimoServiceService {
    private getUrl(): string {
    return environment.apiUrl + "/emprestimos"; // Método que sempre busca o valor atual
  }

    private getToken(): string | null {
    // Recuperar do cookie
    return document.cookie.split('; ').find(row => row.startsWith('jwtToken='))?.split('=')[1] || null;
  }

  constructor(private http: HttpClient) {

        const token = this.getToken();
        if (!token) {
          throw new Error('Token não encontrado');
        }
    
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
          'accept': '*/*',
        });
   }

  obterEmprestimoPorLivro(id: number): Observable<any>{

    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });

    const url = `${this.getUrl()}/livro/${id}`
    return this.http.get<any>(url, { headers });
  }

  obterEmprestimoPorCliente(id: number): Observable<any>{

    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });

    const url = `${this.getUrl()}/cliente/${id}`
    return this.http.get<any>(url, { headers });
  }

  estenderEmprestimo(id: number): Observable<any>{

    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });

    const url = `${this.getUrl()}/estender/${id}`
    return this.http.put<any>(url,null, { headers });
  }

  desativarEmprestimo(id: number): Observable<any>{

    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });

    const url = `${this.getUrl()}/desativar/${id}`
    return this.http.put<any>(url,null, { headers });
  }

  realizarEmprestimo(emprestimo: RealizarEmprestimo): Observable<any>{

    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });

    return this.http.post(this.getUrl(),emprestimo,{ headers });
  }
}
