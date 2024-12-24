using System;
using System.Collections.Generic;

namespace BarbieMovies.Models;

public partial class Movies
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string? Director { get; set; }

    public int? DurationMinutes { get; set; }

    public int? CategoryId { get; set; }

    public virtual Categories? Category { get; set; }
}
