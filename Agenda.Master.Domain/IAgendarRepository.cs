using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Domain
{
    public interface IAgendarRepository
    {
        Agendar Save(Agendar agendar);
        Agendar Get(int id);
        Agendar Update(Agendar agendar);
        Agendar Delete(int i);
        List<Agendar> GetAll();
    }

}
