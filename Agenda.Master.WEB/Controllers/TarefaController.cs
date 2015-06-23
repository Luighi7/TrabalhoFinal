using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda.Master.Domain;
using Agenda.Master.Infra.Data;
using Agenda.Master.Application;

namespace Agenda.Master.WEB.Controllers
{
    public class TarefaController : Controller
    {
        private ITarefaService service = new TarefaService(new TarefaRepository());
        private IAgendarService agendarService = new AgendarService(new AgendarRepository());

        // GET: /Tarefa/
        public ActionResult Index()
        {
            return View(service.RetrieveAll());
        }

        // GET: /Tarefa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;
            Tarefa tarefa = service.Retrieve(i);

            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // GET: /Tarefa/Create
        public ActionResult Create()
        {
            ViewData["AgendaId"] = GetAgendars();

            return View();
        }

        private SelectList GetAgendars()
        {
            var xpto = agendarService.RetrieveAll();
            return new SelectList(xpto, "Id", "Name");
        }

        // POST: /Tarefa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Produto,DataConclusao,AgendaId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                service.Create(tarefa);
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        // GET: /Tarefa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;

            Tarefa tarefa = service.Retrieve(i);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: /Tarefa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Produto,DataConclusao,AgendaId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                service.Update(tarefa);
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        // GET: /Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;

            Tarefa tarefa = service.Retrieve(i);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
