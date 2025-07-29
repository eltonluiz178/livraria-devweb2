import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.prod';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  nomeUsuario: string = '';
  senha: string = '';
  errorMessage: string = '';
  estiloSection: string = "height: 83.6dvh;";

  constructor(private router: Router) { }

  async onSubmit() {
    if (this.nomeUsuario && this.senha) {
      try {
        const response = await fetch(environment.apiUrl + '/api/auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ NomeUsuario: this.nomeUsuario, Senha: this.senha }),
        });

        if (response.ok) {
          const data = await response.json();
          const token = data.token;
          // Armazenar o token nos cookies (opcional, pode ser movido para AppComponent)
          document.cookie = `jwtToken=${encodeURIComponent(token)}; path=/; Secure; SameSite=Strict`;
          console.log('Token armazenado no cookie:', document.cookie);
          // Redirecionar para a página home com o token na query string
          this.router.navigate(['/home']);
          this.errorMessage = '';
        } else {
          this.errorMessage = 'Credenciais inválidas';
        }
      } catch (error) {
        this.errorMessage = 'Erro ao conectar com o servidor';
        console.error('Erro:', error);
      }
    } else {
      this.errorMessage = 'Por favor, preencha todos os campos';
    }
  }
}
