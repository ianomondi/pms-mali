using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models.Queries
{
    public class CompaniesQuery : Query
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }

        public CompaniesQuery(string name, string email, string phoneNumber, string status, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Status = status;
        }
    }
    public class UsersQuery : Query
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public UsersQuery(string username, string name, string email, string phoneNumber, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            UserName = username;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    public class VehiclesQuery : Query
    {
        public string? Owner { get; set; }
        public string? NumberPlate { get; set; }
        public int? NumberOfPasengers { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public double? LifeSpan { get; set; }

        public VehiclesQuery(string owner, string numberPlate,int? numberOfPasengers, DateTime? purchaseDate, double lifespan, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Owner = owner;
            NumberPlate = numberPlate;
            NumberOfPasengers = numberOfPasengers;
            PurchaseDate = purchaseDate;
            LifeSpan = lifespan;
        }
    }

    public class TripsQuery : Query
    {
        public string? Vehicle { get; set; }
        public string? TotalPassangers { get; set; }
        public int? TotalAmount { get; set; }

        public TripsQuery(string vehicle, string totalPassangers, int totalAmount, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Vehicle = vehicle;
            TotalPassangers = totalPassangers;
            TotalAmount = totalAmount;
        }
    }

    public class ExpensesQuery : Query
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalAmount { get; set; }

        public ExpensesQuery(string name, string description, int totalAmount, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Name = name;
            Description = description;
            TotalAmount= totalAmount;
        }
    }
}
