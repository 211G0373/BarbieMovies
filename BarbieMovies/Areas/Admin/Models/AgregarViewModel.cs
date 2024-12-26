using BarbieMovies.Models;

namespace BarbieMovies.Areas.Admin.Models
{
    public class AgregarViewModel
    {
        public Movies Pelicula { get; set; }
        public IEnumerable<Categories>? Categorias { get; set; }

    }
}
