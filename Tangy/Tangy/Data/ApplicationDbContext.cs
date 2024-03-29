﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tangy.Models;

namespace Tangy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<category> categories { get; set; }
        public  DbSet<SubCategory> subCategories { get; set; }
        public DbSet<Menuitem> menuitems { get; set; }
    }
}
