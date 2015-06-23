using Agenda.Master.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Agenda.Master.Domain
{
    public class    Agendar : IObjectValidation
    {
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Data Inicial")]
        public DateTime Datai { get; set; }

        [DisplayName("Data Final")]
        public DateTime Dataf { get; set; }

        public virtual List<Tarefa> Tarefas { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Descricao))
                throw new Exception("Nome Inválido");
            if (DateTime.Today < Datai)
                throw new Exception("Data inicial Inválida");
            if (Datai > Dataf)
                throw new Exception("Data final Inválida");
            }
        }
    }


