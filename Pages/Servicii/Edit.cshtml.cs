using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberBookingWeb.Pages.Servicii
{
    [Authorize(Roles = "Admin")]
    public class EditModel : ServiciuStiluriPageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public EditModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Serviciu Serviciu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serviciu == null)
            {
                return NotFound();
            }

            //var serviciu =  await _context.Serviciu.FirstOrDefaultAsync(m => m.ID == id);
            Serviciu = await _context.Serviciu
               .Include(b => b.Barber)
               .Include(b => b.ServiciuStiluri).ThenInclude(b => b.Stil)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.ID == id);

            if (Serviciu == null)
            {
                return NotFound();
            }

            //Aici este NumeComplet;
            var barberList = _context.Barber.Select(x => new
            {
                x.ID,
                NumeComplet = x.Nume + " " + x.Prenume
            });

            //Serviciu = serviciu;

            ViewData["BarberID"] = new SelectList(_context.Barber, "ID", "ID");
            //ViewData["BarberShopID"] = new SelectList(_context.Set<BarberShop>(), "ID", "BarberShop");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedStiluri)
        {

            if (id == null)
            {
                return NotFound();
            }

            var serviciuToUpdate = await _context.Serviciu
            .Include(i => i.Barber)
            .Include(i => i.ServiciuStiluri)
            .ThenInclude(i => i.Stil)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (serviciuToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Serviciu>(
            serviciuToUpdate,
            "Serviciu",
            i => i.Tip,
            i => i.Descriere,
            i => i.Cost,
            i => i.BarberID))
            {
                UpdateServiciuStiluri(_context, selectedStiluri, serviciuToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateServiciuStiluri(_context, selectedStiluri, serviciuToUpdate);
            PopulateStilAtribuitServiciu(_context, serviciuToUpdate);
            return Page();
        }
    }

} 