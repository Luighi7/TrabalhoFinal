using Agenda.Master.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Application
{
    public interface ITarefaService
    {
        Tarefa Create(Tarefa tarefa);
        Tarefa Retrieve(int id);
        Tarefa Update(Tarefa tarefa);
        Tarefa Delete(int id);
        List<Tarefa> RetrieveAll();
    }
}
