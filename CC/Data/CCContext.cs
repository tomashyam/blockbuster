using Microsoft.EntityFrameworkCore;
using CC.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CC.Data
{
	public class CCContext : DbContext
	{
		public CCContext(DbContextOptions<CCContext> options)
			: base(options)
		{
            if (Database.EnsureCreated())
            {
                InitializeProducts(this);
            }
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Like> Like { get; set; }


        private void InitializeProducts(CCContext ccContext)
        {
            var telAviv = new Cinema
            {
                Name = "Tel Aviv BlockBaster",
                ClosingHour = "22:00",
                OpeningHour = "12:00",
                Latitude = 32.074031,
                Lontitude = 34.792868
            };

            var jerusalem = new Cinema
            {
                Name = "Jerusalem BlockBaster",
                ClosingHour = "22:00",
                OpeningHour = "12:00",
                Latitude = 31.777820,
                Lontitude = 35.209204
            };

            var eilat = new Cinema
            {
                Name = "Eilat BlockBaster",
                ClosingHour = "02:00",
                OpeningHour = "12:00",
                Latitude = 29.556008,
                Lontitude = 34.961806
            };

            var indina = new User
            {
                Gender = Gender.Female,
                Name = "Indina Jons"
            };

            var silivester = new User
            {
                Gender = Gender.Male,
                Name = "Silivester stalon"
            };

            ccContext.User.AddRange(indina, silivester);
            ccContext.Cinema.AddRange(telAviv, jerusalem, eilat);

            var fileEntries = Directory.GetFiles("./movies");
            foreach (string fileName in fileEntries)
            {
                var json = File.ReadAllText(fileName);
                var figures = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
                
                var figuresNoId = figures.Select(x => new Movie
                {
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category,
                    Description = x.Description,
                    Comments = (x.Price < 83 && x.Price > 80) ?
                            new List<Comment>{ new Comment{
                                    Publisher = x.Price % 2 == 0 ? indina : silivester,
                                    Date = DateTime.Now.AddDays(-1),
                                    Text = $"{x.Name} is the greatest!"
                            }} : null
                });               

                Movie.AddRange(figuresNoId);                
            }
            ccContext.SaveChanges();
        }
    }
}