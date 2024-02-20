using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData;

[Index("AgentId", Name = "EmployeesCustomers")]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Please enter a user name.")]
    [StringLength(30)]
    public string Username { get; set; } = null!;

    [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Please enter a first name.")]
    [StringLength(25)]
    public string CustFirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a last name.")]
    [StringLength(25)]
    public string CustLastName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an address.")]
    [StringLength(75)]
    public string CustAddress { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a city.")]
    [StringLength(50)]
    public string CustCity { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a province in an 'AB' format.")]
    [StringLength(2)]
    public string CustProv { get; set; } = null!;


    [Required(ErrorMessage = "Please enter a valid postal code.")]
    [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$",
        ErrorMessage = "Invalid postal code format.")]
    [StringLength(7)]
    public string CustPostal { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a country.")]
    [StringLength(25)]
    public string? CustCountry { get; set; }

    [Required(ErrorMessage = "Please enter a home phone number.")]
    [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Please enter a valid home phone number.")]
    [StringLength(20)]
    public string? CustHomePhone { get; set; }

    [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Please enter a valid business phone number.")]
    [StringLength(20)]
    public string CustBusPhone { get; set; } = null!;

    [StringLength(50)]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string CustEmail { get; set; } = null!;

    public int? AgentId { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("Customers")]
    public virtual Agent? Agent { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Customer")]
    public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomersReward> CustomersRewards { get; set; } = new List<CustomersReward>();
}
