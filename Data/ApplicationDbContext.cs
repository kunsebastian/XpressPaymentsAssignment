﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XpressPayments.Models;

namespace XpressPayments.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationUser> LargeDataSet { get; set; }
    }
}
