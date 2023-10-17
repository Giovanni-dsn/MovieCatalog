using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Data;
using MovieCatalog.Dto;
using MovieCatalog.Models;
using MovieCatalog.Utils;

[ApiController]
[Route("[Controller]")]
public class MovieController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public MovieController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Movie> movies = _dbContext.Movies.ToList();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Movie? movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Create(MovieDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Movie movie = new Movie(request.Title, request.Year, request.Director, request.Description);

        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();

        return StatusCode(201, movie);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MovieDto request)
    {
        Movie existingMovie = _dbContext.Movies.First(x => x.Id == id);

        if (existingMovie == null)
            return NotFound();

        DtoMapper.MapDtoToModel(request, existingMovie);

        _dbContext.Movies.Update(existingMovie);
        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Movie? movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);
        if (movie != null)
        {
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return NoContent();
        }
        return NotFound();
    }
}