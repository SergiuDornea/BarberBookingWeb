using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarberBookingWeb.Data
{
    public class BarberBookingWebContext : IdentityDbContext
    {
        public BarberBookingWebContext (DbContextOptions<BarberBookingWebContext> options)
            : base(options)
        {
        }

        public DbSet<BarberBookingWeb.Models.Barber> Barber { get; set; } = default!;

        public DbSet<BarberBookingWeb.Models.BarberShop>? BarberShop { get; set; }

        public DbSet<BarberBookingWeb.Models.Client>? Client { get; set; }

        public DbSet<BarberBookingWeb.Models.Programare>? Programare { get; set; }

        public DbSet<BarberBookingWeb.Models.Serviciu>? Serviciu { get; set; }
    }
}
