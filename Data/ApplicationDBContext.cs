using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

// it is an giant class to search your relational table
// ApplicationDBContext is your Database Context class.
// A DbContext is a class provided by EF Core that represents a session/connection between your application and the database.
// It is like a bridge between:C# Models (Stock, Comment) and Database Tables (Stocks, Comments)

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        // ctor is a shortened for creating Constructor Automatically
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments {get; set;}
    }
}