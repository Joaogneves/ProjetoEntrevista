import { Component } from '@angular/core';
import { Pessoa, PessoaCadastro } from '../../Model/Pessoa';
import { PessoasService } from '../../Service/pessoas.service';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {
  pessoa: Pessoa;
  pessoaCadastro: PessoaCadastro
  nacionalidadeSelecionada: number;
  nacionalidade = new Map<number, string>();
  pessoas: Pessoa[];
  searchKey:string = '';
  constructor(private service:PessoasService, private router:Router, private location: Location) {
    this.nacionalidade.set(1, 'Brasileiro');
    this.nacionalidade.set(2, 'Outros');
    this.nacionalidadeSelecionada = 1;
    this.pessoa = new Pessoa();
    this.pessoaCadastro = new PessoaCadastro();
    this.pessoas = [];
    this.getAll();
  }

  selectNacionalidade() {
    this.nacionalidadeSelecionada = this.nacionalidadeSelecionada;
  }

  clear() {
    this.pessoaCadastro = new PessoaCadastro();
    this.nacionalidadeSelecionada = 1;
  }

  Create() {
    this.pessoa.Nacionalidade = this.nacionalidadeSelecionada;
    this.service.create(this.pessoaCadastro).pipe(take(1)).subscribe({
      next: (res: Pessoa) => {this.refr()},
      error: (err: HttpErrorResponse) => {
        alert("Dados preenchidos sem respeitar o tamanho máximo de caracteres");
      }
    });
  }

  getAll() {
    this.service.getAll().pipe(take(1)).subscribe({
      next: (res: Pessoa[]) => {this.pessoas = res},
      error: erro => {this.pessoas = []}
    });
  }

  edit(codigo: string) {
    this.router.navigate([`edit/${codigo}`]);
  }

  delete(codigo: string) {
    this.service.delete(codigo).pipe(take(1)).subscribe({
      next: (res) => {this.refr()}
    });
  }

  search() {
    if(this.searchKey === '') {
      this.getAll();
    }
    this.service.getByNome(this.searchKey).pipe(take(1)).subscribe({
      next: (res: Pessoa[]) => {this.pessoas = res},
      error: erro => {this.pessoas = []}
    });
  }

  private refr() {
    window.location.reload();
  }
}