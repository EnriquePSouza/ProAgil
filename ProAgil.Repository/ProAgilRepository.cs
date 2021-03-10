using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }

        //Geral
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
       public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Evento
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        //Palestrante
        public Task<Evento[]> GetAllPalestrantesAsync(bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }
       
        public Task<Evento> GetPalestranteAsyncById(int palestranteId, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }
    }
}