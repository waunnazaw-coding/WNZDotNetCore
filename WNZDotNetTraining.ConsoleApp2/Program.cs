using System.ComponentModel.DataAnnotations;
using WNZDotNetCore.Database.Models;

Console.WriteLine("Hello World");

AppDbContext db  = new AppDbContext();

var  lst = db.TblBlogs.ToList();