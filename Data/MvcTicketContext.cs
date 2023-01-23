using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcTicket.Models;

namespace MvcTicket.Data
{
    public class MvcTicketContext : DbContext
    {
        public MvcTicketContext (DbContextOptions<MvcTicketContext> options)
            : base(options)
        {
        }

        public DbSet<MvcTicket.Models.Ticket> Ticket { get; set; } = default!;
    }
}
