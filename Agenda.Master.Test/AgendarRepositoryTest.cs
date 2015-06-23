using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agenda.Master.Infra.Data;
using System.Data.Entity;
using Agenda.Master.Domain;

namespace Agenda.Master.Test
{
    [TestClass]
    public class AgendarRepositoryTest
    {
        private AgendarContext _contextForTest;

        [TestInitialize]
        public void Setup()
        {
            //Inicializa o banco, apagando e recriando-o
            Database.SetInitializer(new DropCreateDatabaseAlways<AgendarContext>());
            //Seta um registro padrão pra ser usado nos testes
            _contextForTest = new AgendarContext();
            _contextForTest.Agendas.Add(ObjectMother.GetAgenda());
            _contextForTest.SaveChanges();
        }

        [TestMethod]
        public void CreateAgendarRepositoryTest()
        {
            //Arrange
            Agendar b = ObjectMother.GetAgenda();
            IAgendarRepository repository = new AgendarRepository();

            //Action
            Agendar newAgendar = repository.Save(b);

            //Assert
            Assert.IsTrue(newAgendar.Id > 0);
            Assert.IsTrue(newAgendar.Tarefas[0].Id > 0);
        }

        [TestMethod]
        public void RetrieveAgendarRepositoryTest()
        {
            //Arrange
            IAgendarRepository repository = new AgendarRepository();

            //Action
            Agendar agendar = repository.Get(1);

            //Assert
            Assert.IsNotNull(agendar);
            Assert.IsTrue(agendar.Id > 0);
            Assert.IsFalse(string.IsNullOrEmpty(agendar.Descricao));

        }

        [TestMethod]
        public void UpdateAgendarRepositoryTest()
        {
            //Arrange
            IAgendarRepository repository = new AgendarRepository();
            Agendar agendar = _contextForTest.Agendas.Find(1);
            agendar.Descricao = "Teste";
            agendar.Datai = new DateTime(2015, 06, 15, 0, 0, 0);
            agendar.Dataf = new DateTime(2015, 06, 15, 0, 0, 0);

            //Action
            var updatedAgendar = repository.Update(agendar);

            //Assert
            var persistedAgendar = _contextForTest.Agendas.Find(1);
            Assert.IsNotNull(updatedAgendar);
            Assert.AreEqual(updatedAgendar.Id, persistedAgendar.Id);
            Assert.AreEqual(updatedAgendar.Descricao, persistedAgendar.Descricao);
            Assert.AreEqual(updatedAgendar.Datai, persistedAgendar.Datai );
            Assert.AreEqual(updatedAgendar.Dataf, persistedAgendar.Dataf);

        }

        [TestMethod]
        public void DeleteAgendarRepositoryTest()
        {
            //Arrange
            IAgendarRepository repository = new AgendarRepository();

            //Action
            var deletedAgendar = repository.Delete(1);

            //Assert
            var persistedAgendar = _contextForTest.Agendas.Find(1);
            Assert.IsNull(persistedAgendar);
        }
    }
}
