using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MoviesManyToMany.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<ApplicationDbContext>();

            var StarWars = new Movie { Title = "Star Wars" };

            if (!db.Movies.Any())
            {
                // create movies
                db.Movies.AddRange(
                    StarWars,
                    new Movie {Title="Blade Runner"}
                );
                db.SaveChanges();

                // create actors
                db.Actors.AddRange(
                    new Actor {FirstName="Harrison", LastName="Ford"},
                    new Actor {FirstName="Carrie", LastName="Fisher" }
                );
                db.SaveChanges();

                //add many-to - many
                db.MovieActors.AddRange(
                    new MovieActor
                    {
                        MovieId = StarWars.Id,
                        ActorId = db.Actors.FirstOrDefault(a => a.LastName == "Ford").Id
                    },
                    new MovieActor
                    {
                        MovieId = db.Movies.FirstOrDefault(m => m.Title == "Star Wars").Id,
                        ActorId = db.Actors.FirstOrDefault(a => a.LastName == "Fisher").Id
                    },
                    new MovieActor
                    {
                        MovieId = db.Movies.FirstOrDefault(m => m.Title == "Blade Runner").Id,
                        ActorId = db.Actors.FirstOrDefault(a => a.LastName == "Ford").Id
                    }
                );
                db.SaveChanges();

            }
        }
    }
}
