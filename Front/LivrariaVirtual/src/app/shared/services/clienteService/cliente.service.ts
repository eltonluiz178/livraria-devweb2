import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
    private getUrl(): string {
    return environment.apiUrl + "/clientes"; // Método que sempre busca o valor atual
  }

    private getToken(): string | null {
    // Recuperar do cookie
    return document.cookie.split('; ').find(row => row.startsWith('jwtToken='))?.split('=')[1] || null;
  }

  constructor(private http: HttpClient) {}

   obterIdCliente(): Observable<any>{
   
       const token = this.getToken();
       if (!token) {
         throw new Error('Token não encontrado');
       }
   
       const headers = new HttpHeaders({
         'Authorization': `Bearer ${token}`,
         'accept': '*/*',
       });
   
       const url = `${this.getUrl()}/id-cliente`;
       return this.http.get<any>(url, { headers });
     }
}
