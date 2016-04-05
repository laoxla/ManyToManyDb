using MoviesManyToMany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManyToMany.Infrastructure
{
    public class MovieRepository
    {
        private ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddActor(Actor a)
        {
            _db.Actors.Add(a);
            
        }

        public void AddMovieActor(MovieActor ma)
        {
            _db.MovieActors.Add(ma);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return _db.Movies;
        }

        public IQueryable<Movie> FindMoviesByTitle(string search)
        {
            return from m in _db.Movies
                   where m.Title.Contains(search)
                   select m;
        }
        public IQueryable<Movie> FindMoviesByActorName(string actorName)
        {
            return from m in _db.Movies
                   where m.MovieActors.Any(ma => ma.Actor.LastName == actorName)
                   select m;
        }
        public IQueryable<Movie> FindById(int id)
        {
            return from m in _db.Movies
                   where m.Id == id
                   select m;
        }
        
    }
}
