using Agenda.Master.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Application
{
    public interface IAgendarService
    {
        Agendar Create(Agendar agendar);
        Agendar Retrieve(int id);
        Agendar Update(Agendar agendar);
        Agendar Delete(int id);
        List<Agendar> RetrieveAll();
    }
}
