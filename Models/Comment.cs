using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Comment is the child table/entity in the One-to-Many relationship
namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // primary key of Comment table
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        // This is a Foreign Key (FK).
        // Foreign key means: A column in child table that points to the parent table’s primary key.
        // EF Core automatically detects foreign keys based on naming rules (conventions). You don’t always need to explicitly declare it.

        public Stock? Stock{get; set;}
        // This is called a Navigation Property.
        // Definition:
        // A navigation property is used to navigate or access related data easily.
        // So if you have a comment, you can access: Which Stock it belongs to.

    }
}