using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agenda.Master.Domain;
using Moq;
using Agenda.Master.Infra;
using Agenda.Master.Application;

namespace Agenda.Master.Test
{
    [TestClass]
    public class AgendarServiceTest
    {
        [TestMethod]
        public void CreateAgendarServiceValidationAndPersistenceTest()
        {
            //Arrange
            Agendar agendar = ObjectMother.GetAgenda();
            //Fake do repositório
            var repositoryFake = new Mock<IAgendarRepository>();
            repositoryFake.Setup(r => r.Save(agendar)).Returns(agendar);
            //Fake do dominio
            var agendarFake = new Mock<Agendar>();
            agendarFake.As<IObjectValidation>().Setup(b => b.Validate());

            IAgendarService service = new AgendarService(repositoryFake.Object);

            //Action
            service.Create(agendarFake.Object);

            //Assert
            agendarFake.As<IObjectValidation>().Verify(b => b.Validate());
            repositoryFake.Verify(r => r.Save(agendarFake.Object));
        }

        [TestMethod]
        public void RetrieveAgendarServiceTest()
        {
            //Arrange
            Agendar agendar = ObjectMother.GetAgenda();
            //Fake do repositório
            var repositoryFake = new Mock<IAgendarRepository>();
            repositoryFake.Setup(r => r.Get(1)).Returns(agendar);

            IAgendarService service = new AgendarService(repositoryFake.Object);

            //Action
            var agendarFake = service.Retrieve(1);

            //Assert
            repositoryFake.Verify(r => r.Get(1));
            Assert.IsNotNull(agendarFake);
        }

        [TestMethod]
        public void UpdateAgendarServiceValidationAndPersistenceTest()
        {
            //Arrange
            Agendar agendar = ObjectMother.GetAgenda();
            //Fake do repositório
            var repositoryFake = new Mock<IAgendarRepository>();
            repositoryFake.Setup(r => r.Update(agendar)).Returns(agendar);
            //Fake do dominio
            var agendarFake = new Mock<Agendar>();
            agendarFake.As<IObjectValidation>().Setup(b => b.Validate());

            IAgendarService service = new AgendarService(repositoryFake.Object);

            //Action
            service.Update(agendarFake.Object);

            //Assert
            agendarFake.As<IObjectValidation>().Verify(b => b.Validate());
            repositoryFake.Verify(r => r.Update(agendarFake.Object));
        }

        [TestMethod]
        public void DeleteAgendarServiceTest()
        {
            //Arrange
            Agendar agendar = null;
            //Fake do repositório
            var repositoryFake = new Mock<IAgendarRepository>();
            repositoryFake.Setup(r => r.Delete(1)).Returns(agendar);

            IAgendarService service = new AgendarService(repositoryFake.Object);

            //Action
            var agendarFake = service.Delete(1);

            //Assert
            repositoryFake.Verify(r => r.Delete(1));
            Assert.IsNull(agendarFake);
        }
    }
}
