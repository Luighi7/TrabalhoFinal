using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Master.Domain;

namespace Agenda.Master.Test
{
    public class ObjectMother
    {
      public static Agendar GetAgenda()
        {
            Agendar agendar = new Agendar();
            agendar.Descricao = "Segunda Semana Maio";
            agendar.Datai = new DateTime(2015, 06, 10, 0, 0, 0);
            agendar.Dataf = new DateTime(2015, 06, 15, 0, 0, 0);
            agendar.Tarefas = new List<Tarefa>()
            {
                new Tarefa()
                {
                    Descricao = "Suporte Nota Fiscal",
                    Produto = "ERP",
                    DataConclusao = DateTime.Now
                }
            };

            return agendar;
        }

        public static Tarefa GetTarefa()
        {

            Agendar agendar = new Agendar();
            agendar.Descricao = "Terceira Semana de Maio";
            agendar.Datai = new DateTime(2015, 05, 10, 0, 0, 0);
            agendar.Dataf = new DateTime(2015, 05, 15, 0, 0, 0);

            Tarefa tarefa = new Tarefa();
            tarefa.Descricao = "Ajuste Relatório de Contas";
            tarefa.Produto = "Customizado";
            tarefa.DataConclusao = new DateTime(2015, 05, 15, 0, 0, 0);
            tarefa.Agenda = agendar;

            return tarefa;
        } 

    }
}
