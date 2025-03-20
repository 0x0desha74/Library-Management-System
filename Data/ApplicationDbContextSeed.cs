using Bookly.APIs.Entities;
using System.Text.Json;

namespace Bookly.APIs.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task DataSeedAsync(ApplicationDbContext context)
        {



            if (!context.Authors.Any())
            {
                var authorsData = File.ReadAllText("../Bookly.APIs/Data/DataSeed/authors.json");
                var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
                if (authors is not null && authors.Count > 0)
                {
                    foreach (var author in authors)
                        await context.Authors.AddAsync(author);
                }
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                var booksData = File.ReadAllText("../Bookly.APIs/Data/DataSeed/books.json");
                var books = JsonSerializer.Deserialize<List<Book>>(booksData);
                if (books is not null && books.Count > 0)
                {
                    foreach (var book in books)
                        await context.Books.AddAsync(book);
                }
                await context.SaveChangesAsync();
            }




        }
    }
}
