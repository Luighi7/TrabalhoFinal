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
    public class TarefaServiceTest
    {
        [TestMethod]
        public void CreateTarefaServiceValidationAndPersistenceTest()
        {
            //Arrange
            Tarefa tarefa = ObjectMother.GetTarefa();
            //Fake do repositório
            var repositoryFake = new Mock<ITarefaRepository>();
            repositoryFake.Setup(r => r.Save(tarefa)).Returns(tarefa);
            //Fake do dominio
            var postFake = new Mock<Tarefa>();
            postFake.As<IObjectValidation>().Setup(b => b.Validate());

            ITarefaService service = new TarefaService(repositoryFake.Object);

            //Action
            service.Create(postFake.Object);

            //Assert
            postFake.As<IObjectValidation>().Verify(b => b.Validate());
            repositoryFake.Verify(r => r.Save(postFake.Object));
        }

        [TestMethod]
        public void RetrieveTarefaServiceTest()
        {
            //Arrange
            Tarefa tarefa = ObjectMother.GetTarefa();
            //Fake do repositório
            var repositoryFake = new Mock<ITarefaRepository>();
            repositoryFake.Setup(r => r.Get(1)).Returns(tarefa);

            ITarefaService service = new TarefaService(repositoryFake.Object);

            //Action
            var postFake = service.Retrieve(1);

            //Assert
            repositoryFake.Verify(r => r.Get(1));
            Assert.IsNotNull(postFake);
        }

        [TestMethod]
        public void UpdateTarefaServiceValidationAndPersistenceTest()
        {
            //Arrange
            Tarefa tarefa = ObjectMother.GetTarefa();
            //Fake do repositório
            var repositoryFake = new Mock<ITarefaRepository>();
            repositoryFake.Setup(r => r.Update(tarefa)).Returns(tarefa);
            //Fake do dominio
            var postFake = new Mock<Tarefa>();
            postFake.As<IObjectValidation>().Setup(b => b.Validate());

            ITarefaService service = new TarefaService(repositoryFake.Object);

            //Action
            service.Update(postFake.Object);

            //Assert
            postFake.As<IObjectValidation>().Verify(b => b.Validate());
            repositoryFake.Verify(r => r.Update(postFake.Object));
        }

        [TestMethod]
        public void DeleteTarefaServiceTest()
        {
            //Arrange
            Tarefa tarefa = null;
            //Fake do repositório
            var repositoryFake = new Mock<ITarefaRepository>();
            repositoryFake.Setup(r => r.Delete(1)).Returns(tarefa);

            ITarefaService service = new TarefaService(repositoryFake.Object);

            //Action
            var postFake = service.Delete(1);

            //Asserts
            repositoryFake.Verify(r => r.Delete(1));
            Assert.IsNull(postFake);
        }
    }
}
