using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.Data.SqlClient;

namespace BarberBookingWeb.Pages.Servicii
{
    public class IndexModel : PageModel
    {
        private readonly BarberBookingWeb.Data.BarberBookingWebContext _context;

        public IndexModel(BarberBookingWeb.Data.BarberBookingWebContext context)
        {
            _context = context;
        }

        public IList<Serviciu> Serviciu { get; set; } = default!;
        public ServiciuData ServiciuD { get; set; }
        public int ServiciuID { get; set; }
        public int StilID { get; set; }

        public string TipSort { get; set; }
        public string BarberSort { get; set; }
        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? stilID, string sortOrder, string searchString)
        {
            ServiciuD = new ServiciuData();

            TipSort = String.IsNullOrEmpty(sortOrder) ? "tip_desc" : "";
            BarberSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

            ServiciuD.Servicii = await _context.Serviciu
            .Include(b => b.Barber)
            .Include(b => b.ServiciuStiluri)
            .ThenInclude(b => b.Stil)
            .AsNoTracking()
            .OrderBy(b => b.Tip)
            .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ServiciuD.Servicii = ServiciuD.Servicii.Where(s => s.Barber.Prenume.Contains(searchString)

               || s.Barber.Nume.Contains(searchString)
               || s.Tip.Contains(searchString));
            }

            if (id != null)
            {
                ServiciuID = id.Value;
                Serviciu serviciu = ServiciuD.Servicii
                .Where(i => i.ID == id.Value).Single();
                ServiciuD.Stiluri = serviciu.ServiciuStiluri.Select(s => s.Stil);
            }

            switch (sortOrder)
            {
                case "tip_serviciu_desc":
                    ServiciuD.Servicii = ServiciuD.Servicii.OrderByDescending(s =>
                   s.Tip);
                    break;
                case "barber_desc":
                    ServiciuD.Servicii = ServiciuD.Servicii.OrderByDescending(s =>
                   s.Barber.NumeComplet);
                    break;
            }
        }
    }
}