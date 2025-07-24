import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { BibliotecaComponent } from "./pages/biblioteca/biblioteca.component";
import { CabecalhoComponent } from "./shared/componentes/cabecalho/cabecalho.component";
import { LoginComponent } from './pages/login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, BibliotecaComponent, CabecalhoComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  token: string | null = null;

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    // Ouvir mudanças na URL para capturar o token da query string
    this.router.events.subscribe(() => {
      // Obter o primeiro filho (ou o nível atual) para acessar os parâmetros
      const queryParams = this.activatedRoute.snapshot.queryParamMap;
      this.token = queryParams.get('tk');

      if (this.token) {
        console.log('Token capturado:', this.token);
        this.storeToken(this.token);
      } else {
        console.error('Nenhum token fornecido na URL');
      }
    });
  }

  // Função para armazenar o token nos cookies
  private storeToken(token: string) {
    // Armazenar no cookie (remova HttpOnly temporariamente para teste no front-end)
    document.cookie = `jwtToken=${encodeURIComponent(token)}; path=/; Secure; SameSite=Strict`;
    console.log('Token armazenado no cookie:', document.cookie);
  }
}
