using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoviesManyToMany.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesManyToMany.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetActors()
        {
            var actors = _context.Movies.Select(m => new
            {
                MovieId = m.Id,
                Title = m.Title,
                Actors = m.MovieActors.Select( ma => ma.Actor).ToList()
            }).ToList();

            return Ok(actors);
        }


        // POST api/values
        [HttpPost("{id}")]
        public IActionResult PostActor( int id, [FromBody]Actor actor)
        {
            if (!ModelState.IsValid) {
                return HttpBadRequest(ModelState);
            }
            // create actor
            _context.Actors.Add(actor);
            _context.SaveChanges();

            // add actor to existing movie
            _context.MovieActors.Add(new MovieActor
            {
                MovieId = id,
                ActorId = actor.Id
            });
            _context.SaveChanges();

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
