import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Livro } from '../../interfaces/livro';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environment/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class LivroServiceService {
  private url: string = environment.apiUrl + "/livros"

  constructor(private http: HttpClient) {
    console.log('API URL:', this.url);
  }

  obterLivros(): Observable<any>{
    return this.http.get<any>(this.url);
  }
}
