using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcTicket.Data;
using System;
using System.Linq;

namespace MvcTicket.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcTicketContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcTicketContext>>()))
        {
            // Look for any Tickets.
            if (context.Ticket.Any())
            {
                return;   // DB has been seeded
            }
            context.Ticket.AddRange(
                new Ticket
                {
                    Id = 1,
                    ExpirationDate = DateTime.Parse("2023-2-24"),
                    TicketType = "30-dniowy",
                    IfDiscount = false,
                    Price = 110,
                    IfZone = true
                },
                new Ticket
                {
                    Id = 2,
                    ExpirationDate = DateTime.Parse("2023-4-2"),
                    TicketType = "90-dniowy",
                    IfDiscount = false,
                    Price = 280,
                    IfZone = true,
                },
                new Ticket
                {
                    Id = 3,
                    ExpirationDate = DateTime.Parse("2023-1-25"),
                    TicketType = "3-dniowy",
                    IfDiscount = true,
                    Price = 18,
                    IfZone = false,
                },
                new Ticket
                {
                    Id = 4,
                    ExpirationDate = DateTime.Parse("2023-1-23"),
                    TicketType = "20-minutowy",
                    IfDiscount = true,
                    Price = 2,
                    IfZone = false
                },
                new Ticket
                {
                    Id = 5,
                    ExpirationDate = DateTime.Parse("2023-2-17"),
                    TicketType = "30-dniowy",
                    IfDiscount = true,
                    Price = 110,
                    IfZone = true
                },
                new Ticket
                {
                    Id = 6,
                    ExpirationDate = DateTime.Parse("2023-2-5"),
                    TicketType = "30-dniowy",
                    IfDiscount = false,
                    Price = 110,
                    IfZone = false
                }
            );
            context.SaveChanges();
        }
    }
}