using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

// A Model is a C# class that represents the data + rules/structure of the application.
// In simple words:
// 📌 Model = “Data Object” (like Student, Employee, Product)
// Why we use models?
// ✅ To store and transfer data in the app
// ✅ To represent database tables (when using EF Core)
// ✅ To apply validations and business rules

// Types of Models (theory)
// Domain Model / Entity Model
// → represents database tables (used in EF Core)
// View Model
// → used to send data to UI/pages
// DTO (Data Transfer Object)
// → used to transfer clean data between layers / API response

namespace api.Models
{
//     A Stock object represents one row of data.
//     When using Entity Framework Core, this model usually becomes a database table named:
    public class Stock
    {
        public int Id { get; set; }
        // This is the Primary Key (PK) of the table.
        // EF Core has a convention: A property named Id OR StockId is automatically treated as Primary Key.

        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        // means default value is an empty string (to avoid null issues).


        [Column(TypeName = "decimal(18,2)")]
        // This is a Data Annotation Attribute
        public decimal Purchase { get; set; }
        // In C#, an attribute always applies to the very next property/field written after it.
        // EF Core will create the Purchase column in database as: decimal(18,2) :  total 18 digit , Digits after decimal point = 2

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap {get; set;}

        // public List<Comment> Comments {get; set;} = new List<Comment>();
        public List<Comment> Comments {get; set;} = []; // another way of writing above line
        // so Stock table is connected with comment table by one to many relationship
        // A relationship defines how two tables/classes are connected.
        // One object/entity can be related to multiple objects/entities.
        // Parent (One) → Child (Many)


        



    }
}