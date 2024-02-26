using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadastroDePessoas.Models
{
    public class Pessoa
    {
        public int Codigo { get; set; }

        [StringLength(50, ErrorMessage = "O Nome deve possuir no máximo 50 caracteres")]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Inativo { get; set; }
        public short Nacionalidade { get; set; }

        [StringLength(20, ErrorMessage = "O Rg deve possuir no máximo 20 caracteres")]
        public string Rg { get; set; }

        [StringLength(20, ErrorMessage = "O Passaporte deve possuir no máximo 20 caracteres")]
        public string Passaporte { get; set; }


        public Pessoa() { }

        public Pessoa(int codigo, string nome, DateTime dataNascimento, bool inativo, short nacionalidade, string rg, string passaporte)
        {
            Codigo = codigo;
            Nome = nome;
            DataNascimento = dataNascimento;
            Inativo = inativo;
            Nacionalidade = nacionalidade;
            Rg = rg;
            Passaporte = passaporte;
        }
    }
}