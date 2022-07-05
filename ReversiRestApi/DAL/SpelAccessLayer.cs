using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Afx.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.IRepository;

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

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}