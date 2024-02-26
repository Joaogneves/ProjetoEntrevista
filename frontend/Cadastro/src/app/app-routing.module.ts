import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'main', loadChildren: () => import('./Components/main/main.module').then(m => m.MainModule) },
  { path: '', loadChildren: () => import('./Components/main/main.module').then(m => m.MainModule) },
  { path: 'edit/:codigo', loadChildren: () => import('./Components/edit/edit.module').then(m => m.EditModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
