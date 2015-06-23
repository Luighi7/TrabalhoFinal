using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agenda.Master.Infra.Data;
using System.Data.Entity;
using Agenda.Master.Domain;

namespace Agenda.Master.Test
{
    [TestClass]
    public class TarefaRepositoryTest
    {
        private AgendarContext _contextForTest;

        [TestInitialize]
        public void Setup()
        {
            //Inicializa o banco, apagando e recriando-o
            Database.SetInitializer(new DropCreateDatabaseAlways<AgendarContext>());
            //Seta um registro padrão pra ser usado nos testes
            _contextForTest = new AgendarContext();
            _contextForTest.Tarefas.Add(ObjectMother.GetTarefa());
            _contextForTest.SaveChanges();
        }

        [TestMethod]
        public void CreateTarefaRepositoryTest()
        {
            //Arrange
            Tarefa p = ObjectMother.GetTarefa();
            ITarefaRepository repository = new TarefaRepository();

            //Action
            Tarefa newTarefa = repository.Save(p);

            //Assert
            Assert.IsTrue(newTarefa.Id > 0);
        }

        [TestMethod]
        public void UpdateTarefaRepositoryTest()
        {
            //Arrange
            ITarefaRepository repository = new TarefaRepository();
            Agendar agendar = _contextForTest.Agendas.Find(1);
            agendar.Descricao = "Segunda Semana Maio";
            agendar.Datai = new DateTime(2015, 06, 11, 0, 0, 0);
            agendar.Dataf = new DateTime(2015, 06, 16, 0, 0, 0);


            Tarefa tarefa = _contextForTest.Tarefas.Find(1);
            tarefa.Descricao = "Erro Relatório";
            tarefa.Produto = "Financeiro Customizado";
            tarefa.DataConclusao = new DateTime(2015, 06, 15, 0, 0, 0);
            tarefa.Agenda = agendar;

            //Action
            var updatedTarefa = repository.Update(tarefa);

            //Assert
            var persistedTarefa = _contextForTest.Tarefas.Find(1);
            Assert.IsNotNull(updatedTarefa);
            Assert.AreEqual(updatedTarefa.Id, persistedTarefa.Id);
            Assert.AreEqual(updatedTarefa.Descricao, persistedTarefa.Descricao);
            Assert.AreEqual(updatedTarefa.Produto, persistedTarefa.Produto);
            Assert.AreEqual(updatedTarefa.DataConclusao, persistedTarefa.DataConclusao);
            Assert.AreEqual(updatedTarefa.Agenda, persistedTarefa.Agenda);

        }

        [TestMethod]
        public void RetrieveTarefaRepositoryTest()
        {
            //Arrange
            ITarefaRepository repository = new TarefaRepository();

            //Action
            Tarefa tarefa = repository.Get(1);

            //Assert
            Assert.IsNotNull(tarefa);
            Assert.IsTrue(tarefa.Id > 0);
            Assert.IsFalse(string.IsNullOrEmpty(tarefa.Descricao));
            Assert.IsFalse(string.IsNullOrEmpty(tarefa.Produto));

        }

       

        [TestMethod]
        public void DeleteTarefaRepositoryTest()
        {
            //Arrange
            ITarefaRepository repository = new TarefaRepository();

            //Action
            var deletedTarefa = repository.Delete(1);

            //Assert
            var contextForTest = new AgendarContext();
            var persistedTarefa = contextForTest.Tarefas.Find(1);
            Assert.IsNull(persistedTarefa);

        }
    }
}
