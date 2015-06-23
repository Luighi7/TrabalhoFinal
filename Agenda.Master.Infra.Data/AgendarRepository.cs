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
    public class AgendarRepository: IAgendarRepository
    {
        private AgendarContext context;

        public AgendarRepository()
        {
            context = new AgendarContext();
        }

        public Agendar Save(Agendar agendar)
        {
            var newAgendar = context.Agendas.Add(agendar);
            context.SaveChanges();
            return newAgendar;
        }


        public Agendar Get(int id)
        {
            var agendar = context.Agendas.Find(id);
            return agendar;
        }


        public Agendar Update(Agendar agendar)
        {
            DbEntityEntry entry = context.Entry(agendar);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return agendar; 
        }


        public Agendar Delete(int id)
        {
            var agendar = context.Agendas.Find(id);
            DbEntityEntry entry = context.Entry(agendar);
            entry.State = EntityState.Deleted;
            context.SaveChanges();
            return agendar;
        }


        public List<Agendar> GetAll()
        {
            return context.Agendas.ToList();
        }
    }
}
