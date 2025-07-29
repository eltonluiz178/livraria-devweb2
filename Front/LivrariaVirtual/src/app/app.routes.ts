import { Routes } from '@angular/router';
import { BibliotecaComponent } from './pages/biblioteca/biblioteca.component';
import { MeusLivrosComponent } from './pages/meus-livros/meus-livros.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: BibliotecaComponent,
    canActivate: [AuthGuard], // <-- Protegido
  },
  {
    path: 'emprestimos',
    component: MeusLivrosComponent,
    canActivate: [AuthGuard], // <-- Protegido
  }
];
