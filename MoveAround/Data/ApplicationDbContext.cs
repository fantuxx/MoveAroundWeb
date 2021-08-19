using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoveAround.Models;
using MoveAround.Services;

namespace MoveAround.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }//leis dirbti tiesiogiai su Checking db lentele
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<MoveAround.Models.EmailMessage> EmailMessage { get; set; }
        public DbSet<MoveAround.Services.GoogleAdresAPI> GoogleAdresAPI { get; set; }

    }
}
