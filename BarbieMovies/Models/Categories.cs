using System;
using System.Collections.Generic;

namespace BarbieMovies.Models;

public partial class Categories
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movies> Movies { get; set; } = new List<Movies>();
}
