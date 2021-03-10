using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Evento
        Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrantes);
        
        //Palestrante
        Task<Evento[]> GetAllPalestrantesAsync(bool includePalestrantes);
        Task<Evento> GetPalestranteAsyncById(int palestranteId, bool includePalestrantes);
    }
}