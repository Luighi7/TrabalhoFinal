using Agenda.Master.Domain;
using Agenda.Master.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Application
{
    public class TarefaService : ITarefaService
    {
        private ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public Tarefa Create(Tarefa tarefa)
        {
            Validator.Validate(tarefa);

            var savedTarefa = _tarefaRepository.Save(tarefa);

            return savedTarefa;
        }


        public Tarefa Retrieve(int id)
        {
            return _tarefaRepository.Get(id);
        }


        public Tarefa Update(Tarefa tarefa)
        {
            Validator.Validate(tarefa);

            var updatedTarefa = _tarefaRepository.Update(tarefa);

            return updatedTarefa;
        }


        public Tarefa Delete(int id)
        {
            return _tarefaRepository.Delete(id);
        }


        public List<Tarefa> RetrieveAll()
        {
            return _tarefaRepository.GetAll();
        }

    }
}
