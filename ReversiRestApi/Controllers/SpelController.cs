using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.DAL;
using ReversiRestApi.IRepository;

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
        //GET api/spel/getspelbyspelerid/3ru4935utoeidfherjhtnref
        [HttpGet("getSpelBySpelerId/{spelerId}")]
        public Spel GetSpelBySpelerId(string spelerId)
        {
            return iRepository.GetSpel_BySpelerToken(spelerId);
        }

        public class SpelDeelnemen
        {
            public string SpelToken { get; set; }
            public string SpelerToken { get; set; }
        }

        //POST
        [HttpPost("neemDeelAanSpel")]
        public Spel NeemDeelAanSpel([FromBody] SpelDeelnemen neemDeel)
        {
            Debug.WriteLine($"spelToken: {neemDeel.SpelToken}");
            Spel spel = iRepository.GetSpel(neemDeel.SpelToken);
            //iRepository.GetSpel(neemDeel.SpelToken);

            spel.Speler2Token = neemDeel.SpelerToken;
            spel.AandeBeurt = Kleur.Zwart;
            //set the database
            iRepository.UpdateSpel(spel);

            return spel;

        }

        //GET api/spel/spelTokens
        [HttpGet("getSpel")]
        public Spel GetSpel([FromHeader(Name = "x-speltoken")] string spelToken)
        {
            //Debug.WriteLine($"yeet {spelToken}");
            return iRepository.GetSpel(spelToken);
        }
        //GET api/spel/spelTokens
        [HttpGet("getSpel/{spelToken}")]
        public Spel GetSpel_by_SpelToken_in_Url(string spelToken)
        {
            //Debug.WriteLine($"yeet {spelToken}");
            Spel spel = iRepository.GetSpel(spelToken);
            Debug.WriteLine($"spel: {spel}");
            return spel;

        }


        //GET api/spel/spelerToken
        [HttpGet("getSpelBySpelerToken")]
        public Spel GetSpel_ByPlayerToken([FromHeader(Name = "x-spelertoken")] string spelerToken)
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
        public Kleur GetKleur([FromHeader(Name = "x-speltoken")] string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            return spel.AandeBeurt;
        }

        // POST nieuw spel maken
        [HttpPost("createGame")]
        public IActionResult CreateSpel([FromHeader(Name = "x-spelertoken")] string spelerToken, [FromBody] string omschrijving)
        {
            iRepository.AddSpel(new Spel()
            {
                Speler1Token = spelerToken,
                Omschrijving = omschrijving,
            });
            return Ok();
        }

        public class Zet
        {
            public int RijZet { get; set; }
            public int KolomZet { get; set; }
            public bool Pas { get; set; }
        }

        // PUT api/Spel/Zet
        [HttpPut("Zet")]
        public bool PlaatsZet([FromHeader(Name = "x-speltoken")] string spelToken, Zet model)
        {
            Spel spel = iRepository.GetSpel(spelToken);

            if (spel != null)
            {
                if (model.Pas) spel.Pas();
                else spel.DoeZet(model.RijZet, model.KolomZet);

                iRepository.UpdateSpel(spel);
                return true;
            }
            return false;
        }

        //PUT api/spel/Pas
        [HttpPut("pasBeurt/{spelToken}")]
        public bool PasDeBeurt(string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            try
            {
                spel.Pas();
                iRepository.UpdateSpel(spel);
                /*                _context.Update(spel);
                                _context.SaveChanges();*/
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

        }

/*        //PUT api/spel/opgeven
        [HttpPut("geefOp")]
        public void GeefOp([FromHeader( Name = "x-speltoken")] string spelToken)
        {
            Debug.WriteLine($"speltoken: {spelToken}");
            //Spel spel = _context.Spel.Find(spelToken);
            Spel spel2 = iRepository.GetSpel(spelToken);
            //spel.Opgeven(spelerToken);
            //_context.Remove(spel);
            Debug.WriteLine($"spel verwijderd: {spelToken}");
            iRepository.DeleteSpel(spel2);
            //_context.SaveChanges();
        }*/


        [HttpGet("isAfgelopen")]
        public bool IsAfgelopen([FromHeader(Name = "x-spelToken")]string spelToken)
        {
            Debug.WriteLine($"speltoken afgelopen: {spelToken}");
            if (iRepository?.GetSpel(spelToken) != null)
            {
                Spel spel = iRepository.GetSpel(spelToken);
                if (spel.Afgelopen() == true)
                {
                    iRepository.DeleteSpel(spel);
                    return true;
                }
                return false;
            }
            else return true;
        }

        /*// DELETE api/<SpelController>/5
        //------------ VOOR DE BEHEERDER ---------------
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
