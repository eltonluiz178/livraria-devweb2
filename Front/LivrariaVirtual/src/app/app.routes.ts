import { Routes } from '@angular/router';
import { BibliotecaComponent } from './pages/biblioteca/biblioteca.component';
import { MeusLivrosComponent } from './pages/meus-livros/meus-livros.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  { 
    path: 'login',
     component: LoginComponent },
  {
    path: 'home',
    component: BibliotecaComponent,
  },
  {
    path: 'emprestimos',
    component: MeusLivrosComponent
  }
];
