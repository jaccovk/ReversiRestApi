using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.IRepository;
using ReversiRestApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository iRepository;

        public SpelController(ISpelRepository repository)
        {
            iRepository = repository;
        }

        // GET spel omschrijvingen
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get_Spel_Omschrijvingen_Van_Spellen_Met_Wachtende_Speler()
        {
            var spellen = iRepository.GetSpellen()
                .Where(s => s.Speler2Token == null)
                .Select(s => new { s.ID, s.Token, s.Omschrijving });
            return Ok(JsonConvert.SerializeObject(spellen));

        }


        //GET api/spel/spelTokens
        [HttpGet("getSpel")]
        public Spel GetSpel([FromHeader(Name = "x-speltoken")]string spelToken)
        {
            return iRepository.GetSpel(spelToken);
        }


        //GET api/spel/spelerToken
        [HttpGet("getSpelerToken")]
        public Spel GetSpel_ByPlayerToken([FromHeader(Name = "x-spelertoken")]string spelerToken)
        {
            return iRepository.GetSpel_BySpelerToken(spelerToken);
        }


        //GET api/Spel
        [HttpGet("getSpellen")]
        public List<Spel> WaitForSpelers()
        {
            return iRepository.SpellenInDeWacht();
        }


        //GET api/Spel/Beurt
        [HttpGet("getKleur")]
        public Kleur GetKleur([FromHeader(Name = "x-speltoken")]string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            return spel.AandeBeurt;
        }

        // POST nieuw spel maken
        [HttpPost("createGame")]
        public IActionResult CreateSpel([FromHeader(Name ="x-spelertoken")]string spelerToken, [FromBody]string omschrijving)
        {
            iRepository.AddSpel(new Spel()
            {
                Speler1Token = spelerToken,
                Omschrijving = omschrijving,
            });
            return Ok();
        }

        // PUT api/Spel/Zet
        [HttpPut("Zet")]
        public void PlaatsZet(int kolomZet, int rijZet, string spelToken, string spelerToken, bool pas)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            if (pas) spel.Pas();
            else spel.DoeZet(rijZet,kolomZet);
        }

        //PUT api/spel/Pas
        [HttpPut("pasBeurt/{spelToken}/{spelerToken}")]
        public void PasDeBeurt(string spelToken, string spelerToken)
        {
            Spel spel = iRepository.GetSpel(spelToken); 
            spel.Pas();

        }

        //PUT api/spel/opgeven
        [HttpPut("geefOP/{spelToken}/{spelerToken}")]
        public void GeefOp(string spelToken, string spelerToken)
        {
            Spel spel = iRepository.GetSpel(spelToken); 
            spel.Opgeven();

        }


        // DELETE api/<SpelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
