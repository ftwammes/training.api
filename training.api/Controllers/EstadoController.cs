﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly TrainingContext context;

        public EstadoController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Estado>> GetAll()
        {
            IQueryable<Estado> estados = context.Estados;
            return Ok(estados);
        }

        [HttpPost]
        public ActionResult<Estado> CreateEstado(string nome, string sigla)
        {
            var estado = new Estado
            {
                Nome = nome,
                Sigla = sigla
            };
            context.Estados.Add(estado);
            context.SaveChanges();
            return Ok(estado);
        }
        [HttpDelete("{Id}")]
        public void DeleteEstado(long id)
        {
            var estadoDelete = context.Estados.Find(id);
            if (estadoDelete != null)
            {
                context.Estados.Remove(estadoDelete);
                context.SaveChanges();
            }
        }
        [HttpPut("{Id}")]
        public ActionResult<Estado> UpdateEstado(long id, string nome, string sigla)
        {
            var estadoUpdate = context.Estados.Find(id);
            if (estadoUpdate != null)
            {
                estadoUpdate.Nome = nome;
                estadoUpdate.Sigla = sigla;
                context.Estados.Update(estadoUpdate);
                context.SaveChanges();
            }
            return Ok(estadoUpdate);
        }

    }
}