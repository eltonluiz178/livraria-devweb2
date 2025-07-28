import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Livro } from '../../interfaces/livro';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class LivroServiceService {
  private getUrl(): string {
    return environment.apiUrl + "/livros"; // Método que sempre busca o valor atual
  }

  private getToken(): string | null {
    // Recuperar do cookie
    return document.cookie.split('; ').find(row => row.startsWith('jwtToken='))?.split('=')[1] || null;
  }

  constructor(private http: HttpClient) {
  }

  obterLivros(): Observable<any>{
    
    const token = this.getToken();
    if (!token) {
      throw new Error('Token não encontrado');
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'accept': '*/*',
    });
    
    return this.http.get<any>(this.getUrl(), { headers});
  }
}
