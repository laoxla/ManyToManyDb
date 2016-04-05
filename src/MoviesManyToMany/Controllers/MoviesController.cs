using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoviesManyToMany.Models;
using MoviesManyToMany.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesManyToMany.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        
        private MovieRepository _movieRepo;

        public MoviesController(MovieRepository movieRepo)
        {
          
            _movieRepo = movieRepo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetMoviesWithActors()
        {
            var actors = (from m in _movieRepo.GetAllMovies()
                         select new
            {
                MovieId = m.Id,
                Title = m.Title,
                Actors = m.MovieActors.Select( ma => ma.Actor).ToList()
            }).ToList();

            return Ok(actors);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id){
            var movie = (from m in _movieRepo.FindById(id) 
                select new
                {
                    Title = m.Title,
                    Actors = (from ma in m.MovieActors
                              select ma.Actor).ToList()
                }).FirstOrDefault();
            return Ok(movie);
        }


        [HttpGet("byActor/{name}")]
        public IActionResult GetMoviesByActorName(string name)
        {
            //return Ok((from m in _MovieRepo.FindMoviesByActorName(name)
            //           select m).ToList());

            return Ok(_movieRepo.FindMoviesByActorName(name).ToList());
        }




        // POST api/values
        [HttpPost("{id}")]
        public IActionResult PostActor( int id, [FromBody]Actor actor)
        {
            if (!ModelState.IsValid) {
                return HttpBadRequest(ModelState);
            }
            // create actor
            _movieRepo.AddActor(actor);
            _movieRepo.SaveChanges();

            // add actor to existing movie
            _movieRepo.AddMovieActor(new MovieActor
            {
                MovieId = id,
                ActorId = actor.Id
            });
            _movieRepo.SaveChanges();

            // success
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
