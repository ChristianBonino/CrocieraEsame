using Microsoft.AspNetCore.Http;
using Crociera.DB;
using Microsoft.AspNetCore.Mvc;
using Crociera.DB.Entities;
using Crociera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crociera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenotaController : ControllerBase
    {
        // POST api/<Prenota1Controller>
        [HttpPost("InsertPrenotazione")]
        public async Task<IActionResult> Post([FromBody] PrenotazioneModel model)
        {
            if (model != null)
            {
                Locali prenota = new Locali();
                prenota.idVillage = model.idVillage;
                prenota.Nome = model.Nome;
                prenota.Luogo = model.Luogo;
                prenota.Posti = model.Posti;

                String res = this.repository.InsertPerson(prenota);
                var result = new { result = res };

                return Ok(result); // riceve messaggio o errore
            }

            return Ok("Prenotazione non andata a buon fine " + model);
        }

    }

}
