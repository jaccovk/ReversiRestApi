using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            return iRepository.SpellenInDeWacht().Select(a => a.Omschrijving).ToList();
        }


        //GET api/spel/spelToken
        [HttpGet("{spelToken}")]
        public Spel GetSpel(string spelToken)
        {
            return iRepository.GetSpel(spelToken);
        }


        //GET api/spel/spelerToken
        [HttpGet("{spelerToken}")]
        public Spel GetSpel_ByPlayerToken(string spelerToken)
        {
            return iRepository.GetSpel_BySpelerToken(spelerToken);
        }


        //GET api/Spel
        [HttpGet]
        public List<Spel> WaitForSpelers()
        {
            return iRepository.SpellenInDeWacht();
        }


        //GET api/Spel/Beurt
        [HttpGet("{spelToken}")]
        public Kleur GetKleur(string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            return spel.AandeBeurt;
        }

        // POST nieuw spel maken
        [HttpPost("{spelerToken, omschrijving}")]
        public void CreateSpel(string spelerToken, string omschrijving)
        {
            iRepository.AddSpel(new Spel()
            {
                Speler1Token = spelerToken,
                Omschrijving = omschrijving,
            });
        }

        // PUT api/Spel/Zet
        [HttpPut("{kolomZet, rijZet}")]
        public void PlaatsZet(int kolomZet, int rijZet, string spelToken, string spelerToken )
        {
            Spel spel = iRepository.GetSpel(spelToken);
            spel.DoeZet(rijZet,kolomZet);
        }

        //PUT api/spel/opgeven
        [HttpPut("{spelToken,spelerToken}")]
        public void GeefOp(string spelToken, string spelerToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            //spel.Opgeven();

        }


        // DELETE api/<SpelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
