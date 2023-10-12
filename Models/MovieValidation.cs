using Flunt.Notifications;
using Flunt.Validations;
using MovieCatalog.Models;
namespace MovieCatalog.Models;

public class MovieValidation : Notifiable<Notification>
{
    public MovieValidation(string title, DateOnly year, string director, string description)
    {
        // Create validations for constructor parameters
        var contract = new Contract<Movie>()
            .IsNotNullOrEmpty(title, "title")
            .IsGreaterOrEqualsThan(title, 3, "title")
            .IsNotNullOrEmpty(director, "director")
            .IsGreaterOrEqualsThan(director, 3, "director")
            .IsNotNullOrEmpty(description, "description")
            .IsGreaterOrEqualsThan(description, 3, "description");
        AddNotifications(contract);

        Title = title;
        Year = year;
        Director = director;
        Description = description;
    }
    public string Title { get; init; }
    public DateOnly Year { get; init; }
    public string Director { get; init; }
    public string Description { get; init; }

    public static Movie TransferToMovie(MovieValidation validation, Movie movie)
    {
        movie.Title = validation.Title;
        movie.Year = validation.Year;
        movie.Director = validation.Director;
        movie.Description = validation.Description;

        return movie;
    }
}