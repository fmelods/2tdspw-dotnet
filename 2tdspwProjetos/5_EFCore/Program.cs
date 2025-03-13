// See https://aka.ms/new-console-template for more information

using _5_EFCore;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var db = new BloggingContext();
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for a blog");
var blog = await db.Blogs
    .OrderBy(b => b.BlogId)
    .FirstAsync();
Console.WriteLine($"Blog Url: {blog.Url}.");