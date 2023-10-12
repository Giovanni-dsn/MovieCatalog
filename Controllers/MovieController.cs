using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Data;
using MovieCatalog.Models;

[ApiController]
[Route("[Controller]")]
public class MovieController : ControllerBase
{
    [HttpGet] // I created the endpoint just to test the others
    [Route("{id}")]
    public IResult Get([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var movieDb = context.Movies.FirstOrDefault(x => x.Id == id);
        if (movieDb != null) return Results.Ok(movieDb);
        return Results.NotFound();
    }
    [HttpPost]
    public IResult Post([FromBody] MovieRecord request, [FromServices] AppDbContext context)
    {
        MovieValidation validation = new(request.Title, request.Year, request.Director, request.Description);
        if (validation.IsValid)
        {
            Movie movie = MovieValidation.TransferToMovie(validation, new Movie());
            context.Movies.Add(movie);
            context.SaveChanges();
            return Results.Created($"[Controller]/{movie.Id}", movie);
        }
        return Results.BadRequest(validation.Notifications); //Return BadRequest and "Error Message"
    }

    [HttpPut]
    [Route("{id}")]
    public IResult Put([FromRoute] int id, [FromServices] AppDbContext context, [FromBody] MovieRecord request)
    {
        var movieDb = context.Movies.FirstOrDefault(x => x.Id == id);
        MovieValidation validation = new(request.Title, request.Year, request.Director, request.Description);
        if (movieDb != null && validation.IsValid)
        {
            movieDb = MovieValidation.TransferToMovie(validation, movieDb);
            context.Movies.Update(movieDb);
            context.SaveChanges();
        }
        return !validation.IsValid
               ? Results.BadRequest(validation.Notifications)
               : Results.NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public IResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var movieDb = context.Movies.FirstOrDefault(x => x.Id == id);
        if (movieDb != null)
        {
            context.Movies.Remove(movieDb);
            context.SaveChanges();
            return Results.Ok("Successfully deleted");
        }
        return Results.NotFound();
    }
}