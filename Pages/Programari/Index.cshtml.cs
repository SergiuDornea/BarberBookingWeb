using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Pages.Programari
{
    public class IndexModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public IndexModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        public IList<Programare> Programare { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Programare != null)
            {
                Programare = await _context.Programare
                .Include(p => p.Barber)
                .Include(p => p.Client).ToListAsync();
            }
        }
    }
}
