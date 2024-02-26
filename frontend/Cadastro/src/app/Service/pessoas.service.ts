import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pessoa, PessoaCadastro } from '../Model/Pessoa';

@Injectable({
  providedIn: 'root'
})
export class PessoasService {

  private url: string
  constructor(private http:HttpClient) { 
    this.url = 'https://localhost:44375/api/pessoa'
  }

  create(pessoa: PessoaCadastro): Observable<Pessoa> {
    return this.http.post<Pessoa>(this.url, pessoa);
  }

  getAll(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(this.url);
  }

  getByCodigo(codigo: string): Observable<Pessoa> {
    return this.http.get<Pessoa>(`${this.url}/${codigo}`);
  }

  getByNome(nome: string): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(`${this.url}/nome/${nome}`);
  }

  update(pessoa: Pessoa): Observable<Pessoa> {
    return this.http.put<Pessoa>(`${this.url}/${pessoa.Codigo}`, pessoa);
  }

  delete(codigo: string) {
    return this.http.delete(`${this.url}/${codigo}`);
  }
}
