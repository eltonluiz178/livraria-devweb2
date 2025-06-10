import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Livro } from '../../interfaces/livro';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LivroServiceService {
  private readonly API = 'https://localhost:7173/livros'

  constructor(private http: HttpClient) { }

  obterLivros(): Observable<any>{
    return this.http.get<any>(this.API);
  }
}
