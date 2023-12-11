
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        protected readonly MeuDbContext Db;
        public TarefaRepository( MeuDbContext db) : base(db)
        {
            Db = db;
        }

        public async Task Deletar(int id)
        {
            var buscar = await Db.tarefa.FindAsync(id);
            Db.tarefa.Remove(buscar);
          await  Db.SaveChangesAsync();
        }

        public async Task<Tarefa> BuscarPorID(int id)
        {
            return await Db.tarefa.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task<IEnumerable<Tarefa>> BuscarTodasTarefasNaoConcluidas()
        {
            return await Db.tarefa.AsNoTracking().Where(t => t.Concluida.Equals(false)).ToListAsync();
        }
    }
}