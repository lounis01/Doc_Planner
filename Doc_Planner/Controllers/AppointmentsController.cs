using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doc_Planner.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using Doc_Planner.DAL;

namespace Doc_Planner.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DocPlannerContext _context;

        public AppointmentsController(DocPlannerContext context)
        {
            _context = context;
        }

        // GET: Appointments
       [Authorize]
        public async Task<IActionResult> Index(DateTime? dateDebut,DateTime? HDebut, DateTime? dateFin,DateTime? HFin, string nom,DateTime? birthday,bool annules, int page = 1)
        {
            var result = _context.appointments.AsQueryable().AsNoTracking().OrderBy(x=>x.Nom);

            if (dateDebut.HasValue||dateFin.HasValue||nom!=null||birthday!= null||annules!=null)
            {
               

                //Création d'une datedebut et d'une dateFin sur base du datepicker et du hourspicker
                if (dateDebut.HasValue && HDebut.HasValue)
                    dateDebut = new DateTime(dateDebut.Value.Year, dateDebut.Value.Month, dateDebut.Value.Day, HDebut.Value.Hour, HDebut.Value.Minute, HDebut.Value.Second);
                if (dateFin.HasValue && HFin.HasValue)
                    dateFin = new DateTime(dateFin.Value.Year, dateFin.Value.Month, dateFin.Value.Day, HFin.Value.Hour, HFin.Value.Minute, HFin.Value.Second);
              
                if (dateDebut != null)
                    result = result.Where(x => x.HDebutRdv == dateDebut).OrderBy(x => x.Nom);
                if (dateFin != null)
                    result = result.Where(x => x.HFinRdv == dateFin).OrderBy(x => x.Nom);
                if (nom != null)
                    result = result.Where(x => x.Nom == nom).OrderBy(x => x.Nom);
                if (birthday != null)
                    result = result.Where(x => x.Birthday == birthday).OrderBy(x => x.Nom);
                if (annules != null)
                    result = result.Where(x => x.IsDeleted == annules).OrderBy(x => x.Nom);
            }

            var model = await PagingList.CreateAsync(result,5, page);
            return View(model);
    }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HDebutRdv,Nom,Prenom,Sexe,Birthday,Diabete,Poids,Taille,ExamenType,NISS,Telephone")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.BMI = (appointment.Poids / ((appointment.Taille/100) * (appointment.Taille/100)));
                appointment.IsDeleted = false;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HDebutRdv,Nom,Prenom,Sexe,Birthday,Diabete,Poids,Taille,BMI,ExamenType,NISS,Telephone,IsDeleted")] Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.BMI = (appointment.Poids / ((appointment.Taille / 100) * (appointment.Taille / 100)));
                    appointment.IsDeleted = false; 
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.appointments.FindAsync(id);
            //_context.appointments.Remove(appointment);
            //Suppresiion logique comme demandé
            appointment.IsDeleted=true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.appointments.Any(e => e.ID == id);
        }
    }
}
