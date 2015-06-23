using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Domain
{
    public interface ITarefaRepository
    {
        Tarefa Save(Tarefa tarefa);
        Tarefa Get(int id);
        Tarefa Update(Tarefa tarefa);
        Tarefa Delete(int i);

        List<Tarefa> GetAll();
    }
}
