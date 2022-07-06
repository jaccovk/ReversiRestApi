using System.Collections.Generic;
using ReversieISpelImplementatie.Model;

namespace ReversiRestApi.IRepository
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);

        public List<Spel> GetSpellen();

        Spel GetSpel_BySpelerToken(string spelerToken);
        Spel GetSpel(string spelToken);

        public List<Spel> SpellenInDeWacht();
        void DeleteSpel(Spel spel2);
        void UpdateSpel(Spel spel);
    }
}
