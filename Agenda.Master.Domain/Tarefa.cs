using Agenda.Master.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agenda.Master.Domain
{
    public class Tarefa : IObjectValidation
    {
        public int Id { set; get; }

        [DisplayName("Descrição da Tarefa")]
        public string Descricao { get; set; }

        [DisplayName("Produto")]
        public string Produto { get; set; }

        [DisplayName("Data Conclusão")]
        public DateTime DataConclusao { get; set; }

        public int AgendaId { set; get; }

        [DisplayName("Agenda")]
        public virtual Agendar Agenda { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Descricao))
                throw new Exception("Descrição Inválida");
            if (string.IsNullOrEmpty(Produto))
                throw new Exception("Produto Inválido");
            if (DateTime.Today < DataConclusao)
                throw new Exception("Data de Conclusão Inválida");
        }
    }
}
