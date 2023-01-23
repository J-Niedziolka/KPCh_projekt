using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcTicket.Models;

public class ExpDateViewModel
{
    public List<Ticket>? Tickets { get; set; }
    //public SelectList? Genres { get; set; }
    public string? ExpDate { get; set; }
    public string? SearchString { get; set; }
}