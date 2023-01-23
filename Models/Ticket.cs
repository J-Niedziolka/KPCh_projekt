using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTicket.Models;

public class Ticket
{
    public int Id { get; set; }
    [Display(Name = "Expiration Date")]
    [DataType(DataType.Date)]
    public DateTime ExpirationDate { get; set; }
    public string? TicketType { get; set; }
    public bool IfDiscount { get; set; }
    public int Price { get; set; }
    public bool IfZone { get; set; }
}