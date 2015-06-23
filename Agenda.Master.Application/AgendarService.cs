using Agenda.Master.Domain;
using Agenda.Master.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Application
{
    public class AgendarService : IAgendarService
    {
        private IAgendarRepository _agendarRepository;

        public AgendarService(IAgendarRepository agendarRepository)
        {
            _agendarRepository = agendarRepository;
        }

        public Agendar Create(Agendar agendar)
        {
            Validator.Validate(agendar);

            var savedAgendar = _agendarRepository.Save(agendar);

            return savedAgendar;
        }


        public Agendar Retrieve(int id)
        {
            return _agendarRepository.Get(id);
        }


        public Agendar Update(Agendar agendar)
        {
            Validator.Validate(agendar);

            var updatedAgendar = _agendarRepository.Update(agendar);

            return updatedAgendar;
        }


        public Agendar Delete(int id)
        {
            return _agendarRepository.Delete(id);
        }


        public List<Agendar> RetrieveAll()
        {
            return _agendarRepository.GetAll();
        }
    }
}
