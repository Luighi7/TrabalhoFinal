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
    public class AgendarController : Controller
    {
        private IAgendarService service = new AgendarService(new AgendarRepository());

        // GET: /Agendar/
        public ActionResult Index()
        {
            return View(service.RetrieveAll());
        }

        // GET: /Agendar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;
            Agendar agendar = service.Retrieve(i);

            if (agendar == null)
            {
                return HttpNotFound();
            }
            return View(agendar);
        }

        // GET: /Agendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Agendar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,Datai,Dataf")] Agendar agendar)
        {
            if (ModelState.IsValid)
            {
                service.Create(agendar);
                return RedirectToAction("Index");
            }

            return View(agendar);
        }

        // GET: /Agendar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;

            Agendar agendar = service.Retrieve(i);
            if (agendar == null)
            {
                return HttpNotFound();
            }
            return View(agendar);
        }

        // POST: /Agendar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Datai,Dataf")] Agendar agendar)
        {
            if (ModelState.IsValid)
            {
                service.Update(agendar);
                return RedirectToAction("Index");
            }
            return View(agendar);
        }

        // GET: /Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i = (int)id;

            Agendar agendar = service.Retrieve(i);
            if (agendar == null)
            {
                return HttpNotFound();
            }
            return View(agendar);
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