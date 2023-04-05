using ReversieISpelImplementatie.Model;
using ReversiRestApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversiRestApi.Repository
{
    public class SpelRepository : ISpelRepository
    {
        // Lijst met tijdelijke spellen
        public List<Spel> Spellen { get; set; }

        public SpelRepository()
        {
            Spel spel1 = new Spel();
            Spel spel2 = new Spel();
            Spel spel3 = new Spel();

            spel1.Speler1Token = "abcdef";
            spel1.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            spel2.Speler1Token = "ghijkl";
            spel2.Speler2Token = "mnopqr";
            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
            spel3.Speler1Token = "stuvwx";
            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";


            Spellen = new List<Spel> { spel1, spel2, spel3 };
        }

        public void AddSpel(Spel spel)
        {
            Spellen.Add(spel);
        }

        public List<Spel> GetSpellen()
        {
            return Spellen;
        }

        public Spel GetSpel_BySpelerToken(string spelerToken)
        {
            try
            {
                return Spellen.Single(x => x.Speler1Token == spelerToken);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Spel GetSpel(string spelToken)
        {
            try
            {
                return Spellen.Single(x => x.Token == spelToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateSpel(Spel spel)
        {
            Spellen.Remove(Spellen.Single(x => x.Token == spel.Token));
            Spellen.Add(spel);
        }

        public void DeleteSpel(Spel spel)
        {
            Spellen.Remove(spel);
        }

        public List<Spel> SpellenInDeWacht()
        {
            return Spellen.Where(spel => spel.Speler2Token == null).ToList();
        }
    }

}
