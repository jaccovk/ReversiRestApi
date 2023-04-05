using Microsoft.EntityFrameworkCore;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.IRepository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ReversiRestApi.DAL
{
    public class SpelAccessLayer : ISpelRepository
    {
        private SpelDbContext _spelContext;

        public SpelAccessLayer(SpelDbContext context)
        {
            _spelContext = context;
        }

        public void AddSpel(Spel spel)
        {
            _spelContext.Spel.Add(spel);
            _spelContext.SaveChanges();
        }

        public List<Spel> GetSpellen()
        {
            return _spelContext.Spel.ToList();
        }

        public Spel GetSpel_BySpelerToken(string spelerToken)
        {
            return _spelContext.Spel.FirstOrDefault(spel => spel.Speler1Token == spelerToken || spel.Speler2Token == spelerToken);
        }

        public Spel GetSpel(string spelToken)
        {
            return _spelContext.Spel.FirstOrDefault(spel => spel.Token == spelToken);
        }

        public List<Spel> SpellenInDeWacht()
        {
            return _spelContext.Spel.Where(spellen => spellen.Speler2Token == null).ToList();
        }

        public void UpdateSpel(Spel spel)
        {
            _spelContext.Spel.Update(spel);
            _spelContext.SaveChanges();
        }
        public void DeleteSpel(Spel spel)
        {
            if (spel != null)
            {
                _spelContext.Spel.Remove(spel);
                try
                {
                    _spelContext?.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}