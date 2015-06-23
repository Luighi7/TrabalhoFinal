using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agenda.Master.Domain;
using Agenda.Master.Infra;

namespace Agenda.Master.Test
{
    [TestClass]
    public class TarefaDomainTest
    {
        [TestMethod]
        public void CreateATarefaTest()
        {
            Tarefa tarefa = ObjectMother.GetTarefa();

            Assert.IsNotNull(tarefa);
        }

        [TestMethod]
        public void CreateAValidTarefaTest()
        {
            Tarefa tarefa = ObjectMother.GetTarefa();

            Validator.Validate(tarefa);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidTarefaNameTest()
        {
            Tarefa tarefa = new Tarefa();

            Validator.Validate(tarefa);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidTarefaContentTest()
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Descricao = "Ajuste na Interface";

            Validator.Validate(tarefa);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateAInvalidTarefaDateConclusaoTest()
        {
            Tarefa tarefa = new Tarefa();
            tarefa.Descricao = "Ajuste em Relatório";
            tarefa.Produto = "Customizdo";
            tarefa.DataConclusao = DateTime.Now;

            Validator.Validate(tarefa);
        }
    }
}
