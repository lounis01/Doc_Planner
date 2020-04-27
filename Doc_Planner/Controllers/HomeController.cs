
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Doc_Planner.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;


namespace Doc_Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppointmentContext _context;

        public HomeController(AppointmentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            List<Event> listeEvent = new List<Event>();
            foreach (var app in _context.appointments)
            {
                Event ev = new Event();
                //ev.Start = app.HDebutRdv;
                //ev.EventID = app.ID;
                //ev.ThemeColor = "Red";
                //ev.Subject = app.ExamenType;
                ev.EventId = app.ID;
                ev.Title = app.Nom;
                ev.Description = "lol²";
                //ev.Start = "2020-04-24 04:00:00";
                ev.Start = app.HDebutRdv.ToString("yyyy-MM-dd hh:mm:ss");
                ev.End = "LOOOOOOOOOOL";
                ev.AllDay = false;
                ev.Color = "green";



                listeEvent.Add(ev);
            }

            var events = listeEvent;
            //return new System.Web.Mvc.JsonResult { Data = events, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };
            return Json(events);

        }

    }
    
}
