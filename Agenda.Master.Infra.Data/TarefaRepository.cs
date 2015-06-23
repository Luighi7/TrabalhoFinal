using Agenda.Master.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Infra.Data
{
    public class TarefaRepository : ITarefaRepository
    {
        private AgendarContext context;

        public TarefaRepository()
        {
            context = new AgendarContext();
        }

        public Tarefa Save(Tarefa tarefa)
        {
            var newTarefa = context.Tarefas.Add(tarefa);
            context.SaveChanges();
            return newTarefa;
        }


        public Tarefa Get(int id)
        {
            var tarefa = context.Tarefas.Find(id);
            return tarefa;
        }


        public Tarefa Update(Tarefa tarefa)
        {
            DbEntityEntry entry = context.Entry(tarefa);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return tarefa;
        }


        public Tarefa Delete(int id)
        {
            var tarefa = context.Tarefas.Find(id);
            DbEntityEntry entry = context.Entry(tarefa);
            entry.State = EntityState.Deleted;
            context.SaveChanges();
            return tarefa;
        }

        public List<Tarefa> GetAll()
        {
            return context.Tarefas.ToList();
        }
    }
}
