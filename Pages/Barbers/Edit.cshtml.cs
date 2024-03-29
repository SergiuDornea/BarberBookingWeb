﻿using System;
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

namespace BarberBookingWeb.Pages.Barbers
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public EditModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Barber Barber { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Barber == null)
            {
                return NotFound();
            }

            var barber =  await _context.Barber.FirstOrDefaultAsync(m => m.ID == id);
            if (barber == null)
            {
                return NotFound();
            }
            Barber = barber;
           ViewData["BarberShopID"] = new SelectList(_context.BarberShop, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Barber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarberExists(Barber.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BarberExists(int id)
        {
          return (_context.Barber?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
