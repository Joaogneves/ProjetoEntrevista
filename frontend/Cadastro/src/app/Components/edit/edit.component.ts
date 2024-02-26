import { Component } from '@angular/core';
import { Pessoa } from '../../Model/Pessoa';
import { PessoasService } from '../../Service/pessoas.service';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent {
  pessoa: Pessoa;
  nacionalidadeSelecionada: number;
  nacionalidade = new Map<number, string>();
  cod: string;
  constructor(private service:PessoasService, private router:Router, activateRoute: ActivatedRoute) {
    this.nacionalidade.set(1, 'Brasileiro')
    this.nacionalidade.set(2, 'Outros')
    this.nacionalidadeSelecionada = 1;
    this.pessoa = new Pessoa()
    this.cod = activateRoute.snapshot.paramMap.get("codigo")!;
    this.getByCodigo(this.cod);
  }

  getByCodigo(codigo: string) {
    this.service.getByCodigo(codigo).pipe(take(1)).subscribe({
      next: (res: Pessoa) => {this.pessoa = res, console.log(this.pessoa),
      this.pessoa.DataNascimento = this.pessoa.DataNascimento.split('T')[0], this.nacionalidadeSelecionada = this.pessoa.Nacionalidade},
      error: (err) => {}
    })
  }


  selectNacionalidade() {
    this.nacionalidadeSelecionada = this.nacionalidadeSelecionada
  }

  goHome() {
    this.router.navigate(['/']);
  }

  update() {
    this.pessoa.Nacionalidade = this.nacionalidadeSelecionada;
    this.service.update(this.pessoa).pipe(take(1)).subscribe({
      next: (res: Pessoa) => {this.router.navigate(['/'])},
      error: err => {
        alert("Dados preenchidos sem respeitar o tamanho máximo de caracteres");
      }
    })
  }
}
