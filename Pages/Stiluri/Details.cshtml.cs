using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Pages.Stiluri
{
    public class DetailsModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public DetailsModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

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
    }
}
