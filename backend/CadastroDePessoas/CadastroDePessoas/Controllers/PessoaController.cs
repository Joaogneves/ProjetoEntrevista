using CadastroDePessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CadastroDePessoas.Controllers
{
    public class PessoaController : ApiController
    {
        Repositories.Pessoa _repositories;

        public PessoaController()
        {
            _repositories = new Repositories.Pessoa(Configurations.Databases.getConnectionString());
        }

        [HttpGet]
        [Route("api/pessoa")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<Models.Pessoa> pessoas = await _repositories.GetAll();
                if(pessoas.Count <=0)
                {
                    return NotFound();
                }
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // GET: api/Pessoa/5
        [HttpGet]
        [Route("api/pessoa/{codigo}")]
        public async Task<IHttpActionResult> GetByCode(int codigo)
        {
            try
            {
                Models.Pessoa pessoa = await _repositories.GetByCodigo(codigo);
                if (pessoa.Codigo == 0)
                {
                    return NotFound();
                }
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/pessoa/nome/{nome}")]
        public async Task<IHttpActionResult> GetByName(string nome)
        {
            try
            {
                List<Models.Pessoa> pessoas = await _repositories.GetByName(nome);
                if (pessoas.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/pessoa")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Pessoa pessoa)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                int codigo = await _repositories.Create(pessoa);
                pessoa.Codigo = codigo;
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT: api/Pessoa/5
        [HttpPut]
        [Route("api/pessoa/{codigo}")]
        public async Task<IHttpActionResult> Put(int codigo, [FromBody]Models.Pessoa pessoa)
        {
            if (codigo != pessoa.Codigo)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                int res = await _repositories.Update(pessoa);
                if (res <= 0)
                    return NotFound();
                pessoa.Codigo = codigo;
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Pessoa/5

        [HttpDelete]
        [Route("api/pessoa/{codigo}")]
        public async Task<IHttpActionResult> Delete(string codigo)
        {
            int res = await _repositories.Delete(codigo);
                if (res <= 0)
                    return NotFound();
                return Ok();
        }
    }
}
