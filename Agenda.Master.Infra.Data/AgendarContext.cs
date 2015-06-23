using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Master.Domain;


namespace Agenda.Master.Infra.Data
{
    public class AgendarContext : DbContext
    {
        public AgendarContext()
            : base("AgendarContext")
        {
            
        }

        public DbSet<Agendar> Agendas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendar>().ToTable("TBAgenda");
            modelBuilder.Entity<Agendar>()
                .Property(b => b.Descricao)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Tarefa>().ToTable("TBTarefa");
            modelBuilder.Entity<Tarefa>()
                .Property(b => b.Descricao)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Tarefa>()
           .Property(b => b.Produto)
           .IsRequired()
           .HasMaxLength(2000);
        }

    }

}
