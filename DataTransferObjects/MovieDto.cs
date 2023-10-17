#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Dto;

public class MovieDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    public DateOnly Year { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Director { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 60)]
    public string Description { get; set; }
}