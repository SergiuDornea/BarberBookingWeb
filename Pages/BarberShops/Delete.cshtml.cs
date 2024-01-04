using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberBookingWeb.Pages.BarberShops
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public DeleteModel(BarberBookingWeb.Data.BarberBookingWebContext context)
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

            var barbershop = await _context.BarberShop.FirstOrDefaultAsync(m => m.ID == id);

            if (barbershop == null)
            {
                return NotFound();
            }
            else 
            {
                BarberShop = barbershop;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BarberShop == null)
            {
                return NotFound();
            }
            var barbershop = await _context.BarberShop.FindAsync(id);

            if (barbershop != null)
            {
                BarberShop = barbershop;
                _context.BarberShop.Remove(BarberShop);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
