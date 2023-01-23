using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcTicket.Models;

public class TicketTypeViewModel
{
    public List<Ticket>? Tickets { get; set; }
    public SelectList? Types { get; set; }
    public string? TicketType { get; set; }
    public string? SearchString { get; set; }
}