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

namespace BarberBookingWeb.Pages.Stiluri
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
      public Stil Stil { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stil == null)
            {
                return NotFound();
            }

            var stil = await _context.Stil.FirstOrDefaultAsync(m => m.ID == id);

            if (stil == null)
            {
                return NotFound();
            }
            else 
            {
                Stil = stil;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stil == null)
            {
                return NotFound();
            }
            var stil = await _context.Stil.FindAsync(id);

            if (stil != null)
            {
                Stil = stil;
                _context.Stil.Remove(Stil);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
