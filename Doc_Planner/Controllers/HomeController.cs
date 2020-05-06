
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Doc_Planner.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Doc_Planner.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Doc_Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly DocPlannerContext _context;

        public HomeController(DocPlannerContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public JsonResult GetEvents()
        {
            List<Event> listeEvent = new List<Event>();
            foreach (var app in _context.appointments)
            {
                Event ev = new Event();
               
                ev.EventId = app.ID;
                ev.Title =  app.ExamenType + " | " + app.Nom+" " +app.Prenom;
                ev.Description = app.ExamenType;
                ev.Start = app.HDebutRdv;
                ev.End = app.HFinRdv; 
                ev.AllDay = false;
                if (app.HopitalDeRef == "GHDC") { ev.Color = "lightgreen"; }
                else if (app.HopitalDeRef == "CNDG"){ev.Color = "cyan";}
                else { ev.Color = "pink"; }


                listeEvent.Add(ev);
            }

            var events = listeEvent;
            return Json(events);

        }



    }
    
}
