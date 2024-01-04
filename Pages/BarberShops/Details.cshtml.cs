using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Pages.BarberShops
{
    public class DetailsModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public DetailsModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

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
    }
}
