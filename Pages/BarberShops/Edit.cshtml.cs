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

namespace BarberBookingWeb.Pages.BarberShops
{
    public class EditModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public EditModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BarberShop BarberShop { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BarberShop == null)
            {
                return NotFound();
            }

            var barbershop =  await _context.BarberShop.FirstOrDefaultAsync(m => m.ID == id);
            if (barbershop == null)
            {
                return NotFound();
            }
            BarberShop = barbershop;
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

            _context.Attach(BarberShop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarberShopExists(BarberShop.ID))
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

        private bool BarberShopExists(int id)
        {
          return (_context.BarberShop?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
