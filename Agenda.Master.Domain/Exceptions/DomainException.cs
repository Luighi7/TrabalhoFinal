using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Master.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string msg)
            : base(msg)
        {
        }
    }
}
