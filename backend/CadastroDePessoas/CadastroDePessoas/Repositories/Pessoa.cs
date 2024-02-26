using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CadastroDePessoas.Repositories
{
    public class Pessoa
    {
        public readonly string _connectioString;
        public SqlConnection _conn;
        readonly SqlCommand _cmd;

        private string buscaPadrao = "SELECT Codigo, Nome, DataNascimento, Inativo, Nacionalidade, RG, Passaporte FROM Pessoas";

        public Pessoa(String connectionString)
        {
            _connectioString = connectionString;
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand();
        }

        public async Task<int> Create(Models.Pessoa pessoa)
        {
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "INSERT INTO Pessoas(Nome, DataNascimento, Inativo, Nacionalidade, RG, Passaporte)" +
                        " values (@Nome, @DataNascimento, @Inativo, @Nacionalidade, @RG, @Passaporte);SELECT Convert(int, SCOPE_IDENTITY());";
                    _cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = pessoa.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@DataNascimento", SqlDbType.VarChar)).Value = pessoa.DataNascimento;
                    _cmd.Parameters.Add(new SqlParameter("@Inativo", SqlDbType.Bit)).Value = pessoa.Inativo;
                    _cmd.Parameters.Add(new SqlParameter("@Nacionalidade", SqlDbType.SmallInt)).Value = pessoa.Nacionalidade;
                    _cmd.Parameters.Add(new SqlParameter("@RG", SqlDbType.VarChar)).Value = pessoa.Rg;
                    _cmd.Parameters.Add(new SqlParameter("@Passaporte", SqlDbType.VarChar)).Value = pessoa.Passaporte;
                    _cmd.Connection = _conn;
                    var res = await _cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(res);
                }
            }
        }

        public async Task<List<Models.Pessoa>> GetAll()
        {
            List<Models.Pessoa> pessoas = new List<Models.Pessoa>();
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "SELECT Codigo, Nome, DataNascimento, Inativo, Nacionalidade, RG, Passaporte FROM Pessoas;";
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        Models.Pessoa p = new Models.Pessoa();
                        p.Codigo = (int)dr["Codigo"];
                        p.Nome = (string)dr["Nome"];
                        p.DataNascimento = (DateTime)dr["DataNascimento"];
                        p.Inativo = (bool)dr["Inativo"];
                        p.Nacionalidade = (short)dr["Nacionalidade"];
                        p.Rg = (string)dr["RG"];
                        p.Passaporte = (string)dr["Passaporte"];
                        pessoas.Add(p);
                    }
                    return pessoas;
                }
            }
        }

        public async Task<List<Models.Pessoa>> GetByName(string nome)
        {
            List<Models.Pessoa> pessoas = new List<Models.Pessoa>();
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "SELECT * FROM Pessoas WHERE Nome LIKE @Nome;";
                    _cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        Models.Pessoa p = new Models.Pessoa();
                        p.Codigo = (int)dr["Codigo"];
                        p.Nome = (string)dr["Nome"];
                        p.DataNascimento = (DateTime)dr["DataNascimento"];
                        p.Inativo = (bool)dr["Inativo"];
                        p.Nacionalidade = (short)dr["Nacionalidade"];
                        p.Rg = (string)dr["RG"];
                        p.Passaporte = (string)dr["Passaporte"];
                        pessoas.Add(p);
                    }
                    return pessoas;
                }
            }
        }

        public async Task<Models.Pessoa> GetByCodigo(int codigo)
        {
            Models.Pessoa pessoa = new Models.Pessoa();
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "SELECT Codigo, Nome, DataNascimento, Inativo, Nacionalidade, RG, Passaporte FROM Pessoas WHERE Codigo = @Codigo;";
                    _cmd.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar)).Value = codigo;
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        pessoa.Codigo = (int)dr["Codigo"];
                        pessoa.Nome = (string)dr["Nome"];
                        pessoa.DataNascimento = (DateTime)dr["DataNascimento"];
                        pessoa.Inativo = (bool)dr["Inativo"];
                        pessoa.Nacionalidade = (short)dr["Nacionalidade"];
                        pessoa.Rg = (string)dr["RG"];
                        pessoa.Passaporte = (string)dr["Passaporte"];
                    }
                    return pessoa;
                }
            }
        }

        public async Task<int> Update(Models.Pessoa pessoa)
        {
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "UPDATE Pessoas set Nome = @Nome, DataNascimento = @DataNascimento, Inativo = @Inativo," +
                        "Nacionalidade = @Nacionalidade, RG = @RG, Passaporte = @Passaporte WHERE Codigo  = @Codigo";
                    _cmd.Parameters.Add(new SqlParameter("Codigo", SqlDbType.VarChar)).Value = pessoa.Codigo;
                    _cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = pessoa.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@DataNascimento", SqlDbType.VarChar)).Value = pessoa.DataNascimento;
                    _cmd.Parameters.Add(new SqlParameter("@Inativo", SqlDbType.Bit)).Value = pessoa.Inativo;
                    _cmd.Parameters.Add(new SqlParameter("@Nacionalidade", SqlDbType.SmallInt)).Value = pessoa.Nacionalidade;
                    _cmd.Parameters.Add(new SqlParameter("@RG", SqlDbType.VarChar)).Value = pessoa.Rg;
                    _cmd.Parameters.Add(new SqlParameter("@Passaporte", SqlDbType.VarChar)).Value = pessoa.Passaporte;
                    _cmd.Connection = _conn;
                    int res = await _cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(res);
                }
            }
        }

        public async Task<int> Delete(string codigo)
        {
            using (_conn)
            {
                _conn.Open();
                using (_cmd)
                {
                    _cmd.CommandText = "DELETE Pessoas WHERE Codigo = @Codigo;";
                    _cmd.Parameters.Add(new SqlParameter("Codigo", SqlDbType.VarChar)).Value = codigo;
                    _cmd.Connection = _conn;
                    int res = await _cmd.ExecuteNonQueryAsync();
                    return res;
                }
            }

        }
    }
}