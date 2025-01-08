namespace BarbieMovies.Models.ViewModels
{
    public class BusquedaViewModel
    {
        public IEnumerable<Movies> peliculas { get; set; }
        public string busqueda {  get; set; }
    }
}
