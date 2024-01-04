using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberBookingWeb.Pages.BarberShops
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public CreateModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BarberShop BarberShop { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BarberShop == null || BarberShop == null)
            {
                return Page();
            }

            _context.BarberShop.Add(BarberShop);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
