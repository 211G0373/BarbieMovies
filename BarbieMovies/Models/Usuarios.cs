using System;
using System.Collections.Generic;

namespace BarbieMovies.Models;

public partial class Usuarios
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
