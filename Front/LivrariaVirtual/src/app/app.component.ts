import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { BibliotecaComponent } from "./pages/biblioteca/biblioteca.component";
import { CabecalhoComponent } from "./shared/componentes/cabecalho/cabecalho.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, BibliotecaComponent, CabecalhoComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'LivrariaVirtual';
}
