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
        public async Task<IActionResult> Index(DateTime? dateDebut,DateTime? HDebut,string nom,string examentype,bool urgent,string hopitalref,bool annules, int page = 1)
        {
            var result = _context.appointments.AsQueryable().AsNoTracking().OrderBy(x=>x.HDebutRdv);

            if (dateDebut.HasValue || nom != null || examentype != null || hopitalref != null || annules == true || urgent == true)
            {


                //Création d'une datedebut et d'une dateFin sur base du datepicker et du hourspicker
                if (dateDebut.HasValue && HDebut.HasValue)
                {
                    dateDebut = new DateTime(dateDebut.Value.Year, dateDebut.Value.Month, dateDebut.Value.Day, HDebut.Value.Hour, HDebut.Value.Minute, HDebut.Value.Second);
                }

                if (dateDebut != null)
                {
                    if (annules == false) { result = result.Where(x => x.HDebutRdv == dateDebut && x.IsDeleted == false).OrderBy(x => x.HDebutRdv); }
                    else { result = result.Where(x => x.HDebutRdv == dateDebut).OrderBy(x => x.HDebutRdv); }
                }

                if (nom != null)
                {
                    if (annules == false) { result = result.Where(x => x.Nom == nom && x.IsDeleted == false).OrderBy(x => x.HDebutRdv); }
                    else { result = result.Where(x => x.Nom == nom).OrderBy(x => x.HDebutRdv); }
                }
                if (examentype != null)
                {
                    if (annules == false) { result = result.Where(x => x.ExamenType == examentype && x.IsDeleted == false).OrderBy(x => x.HDebutRdv); }
                    else { result = result.Where(x => x.ExamenType == examentype).OrderBy(x => x.HDebutRdv); }
                }
                if (urgent == true)
                {
                    if (annules == false) { result = result.Where(x => x.Emergency == true && x.IsDeleted == false).OrderBy(x => x.HDebutRdv); }
                    else { result = result.Where(x => x.Emergency == true).OrderBy(x => x.HDebutRdv); }
                }
                if (hopitalref != null)
                {
                    if (annules == false) { result = result.Where(x => x.HopitalDeRef == hopitalref && x.IsDeleted == false).OrderBy(x => x.HDebutRdv); }
                    else { result = result.Where(x => x.HopitalDeRef == hopitalref).OrderBy(x => x.HDebutRdv); }
                }
                if (annules == true)
                {
                    result = result.Where(x => x.IsDeleted == true).OrderBy(x => x.HDebutRdv);
                }
            }
            else 
            {
                result = _context.appointments.AsQueryable().AsNoTracking().Where(x => x.IsDeleted == false).OrderBy(x => x.HDebutRdv);

            }
            var model = await PagingList.CreateAsync(result,4, page);
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
        public async Task<IActionResult> Create([Bind("ID,HDebutRdv,Nom,Prenom,Sexe,Birthday,Diabete,Poids,Taille,ExamenType,NISS,Telephone,HopitalDeRef")] Appointment appointment,DateTime heure)
        {
            if (ModelState.IsValid)
            {
                appointment.HDebutRdv= new DateTime(appointment.HDebutRdv.Year, appointment.HDebutRdv.Month, appointment.HDebutRdv.Day, heure.Hour, heure.Minute, heure.Second);
                appointment.HFinRdv = appointment.HDebutRdv.AddMinutes(20);
                appointment.BMI = (appointment.Poids / ((appointment.Taille/100) * (appointment.Taille/100)));
                appointment.BMI = (float)System.Math.Round(appointment.BMI, 2);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,HDebutRdv,Nom,Prenom,Sexe,Birthday,Diabete,Poids,Taille,ExamenType,NISS,Telephone,HopitalDeRef")] Appointment appointment,DateTime heure)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.HDebutRdv = new DateTime(appointment.HDebutRdv.Year, appointment.HDebutRdv.Month, appointment.HDebutRdv.Day, heure.Hour, heure.Minute, heure.Second);
                    appointment.HFinRdv = appointment.HDebutRdv.AddMinutes(20);
                    appointment.BMI = (appointment.Poids / ((appointment.Taille / 100) * (appointment.Taille / 100)));
                    appointment.BMI = (float)System.Math.Round(appointment.BMI, 2);
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
