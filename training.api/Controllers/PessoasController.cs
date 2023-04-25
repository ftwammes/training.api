﻿using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly TrainingContext context;

        public PessoasController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetAll(string? nome = null, Sexo? sexo = null)
        {
            IQueryable<Pessoa> pessoas = context.Pessoas;
            if(nome != null) {
                pessoas = pessoas.Where(x => x.Nome.Contains(nome));
            }
            if(sexo != null)
            {
                pessoas = pessoas.Where(x => x.Sexo == sexo);
            }
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Pessoa>> GetById(long id)
        {
            IQueryable<Pessoa> pessoas = context.Pessoas;
            if (id > 0)
            {
                pessoas = pessoas.Where(x => x.Id == id);
            }
            return Ok(pessoas);
        }

        [HttpPost]
        public ActionResult<Pessoa> CreatePessoa(Sexo sexo, string? nome = null, string? telefone = null)
        {
            var pessoa = new Pessoa
            {
                Nome = nome,
                Telefone = telefone,
                Sexo = sexo
            };
            context.Pessoas.Add(pessoa);
            context.SaveChanges();
            return Ok(pessoa);
        }
        [HttpDelete]
        public void DeletePessoa(long id)
        {
            var pessoaDelete = context.Pessoas.Find(id);
            if(pessoaDelete != null)
            {
                context.Pessoas.Remove(pessoaDelete);
                context.SaveChanges();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Pessoa> UpdatePessoa(long id, string nome, string telefone, Sexo sexo)
        {
            var pessoaUpdate = context.Pessoas.Find(id);
            if (pessoaUpdate != null)
            {
                pessoaUpdate.Nome = nome;
                pessoaUpdate.Telefone = telefone;
                pessoaUpdate.Sexo = sexo;
                context.Pessoas.Update(pessoaUpdate);
                context.SaveChanges();
            }
            return Ok(pessoaUpdate);
        }

    }
}
