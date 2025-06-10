import { Routes } from '@angular/router';
import { BibliotecaComponent } from './pages/biblioteca/biblioteca.component';
import { MeusLivrosComponent } from './pages/meus-livros/meus-livros.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: BibliotecaComponent,
  },
  {
    path: 'emprestimos',
    component: MeusLivrosComponent
  }
];
