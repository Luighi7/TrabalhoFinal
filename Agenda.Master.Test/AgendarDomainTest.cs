using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agenda.Master.Infra;
using Agenda.Master.Domain;

namespace Agenda.Master.Test
{
    [TestClass]
    public class AgendarDomainTest
    {
        [TestMethod]
        public void CreateAAgendaTest()
        {
            Agendar agendar = ObjectMother.GetAgenda();

            Assert.IsNotNull(agendar);
        }

        [TestMethod]
        public void CreateAValidAgendaTest()
        {
            Agendar agendar = ObjectMother.GetAgenda();

            Validator.Validate(agendar);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidAgendaDescricaoCorretaTest()
        {
            Agendar agendar = new Agendar();

            Validator.Validate(agendar);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidAgendaDataInicialTest()
        {
            Agendar agendar = new Agendar();
            agendar.Datai = new DateTime(2015, 06, 10, 0, 0, 0);

            Validator.Validate(agendar);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidPostDateFinalTest()
        {
            Agendar agendar = new Agendar();
            agendar.Descricao = "Segunda Semana Maio";
            agendar.Datai = new DateTime(2015, 06, 18, 0, 0, 0);
            agendar.Dataf = new DateTime(2015, 06, 16, 0, 0, 0);

            Validator.Validate(agendar);
        }
    }
}
