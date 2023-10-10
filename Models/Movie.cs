#pragma warning disable CS8618

namespace MovieCatalog.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly Year { get; set; }
    public string Director { get; set; }
    public string Description { get; set; }

    public Movie()  {  } // Empty constructor for EF. (warning disable: CS8618)

    public Movie(string title, DateOnly year, string director, string description)
    {
        Title = title;
        Year = year;
        Director = director;
        Description = description;
    }
}