export class Pessoa {
    Codigo: string
    Nome: string
    DataNascimento: string
    Inativo: boolean
    Nacionalidade: number
    Rg: string
    Passaporte: string

    constructor() {
        this.Codigo = ''
        this.Nome = ''
        this.DataNascimento = ''
        this.Inativo = false
        this.Nacionalidade = 1
        this.Rg = ''
        this.Passaporte = ''
    }
}

export class PessoaCadastro {
    Nome: string
    DataNascimento: string
    Inativo: boolean
    Nacionalidade: number
    Rg: string
    Passaporte: string

    constructor() {
        this.Nome = ''
        this.DataNascimento = ''
        this.Inativo = false
        this.Nacionalidade = 1
        this.Rg = ''
        this.Passaporte = ''
    }
}