using ReversieISpelImplementatie.Model;
using System.Collections.Generic;

namespace ReversiRestApi.IRepository
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);

        public List<Spel> GetSpellen();

        Spel GetSpel_BySpelerToken(string spelerToken);
        Spel GetSpel(string spelToken);

        public List<Spel> SpellenInDeWacht();
        void DeleteSpel(Spel spel);
        void UpdateSpel(Spel spel);
    }
}
