namespace Cinema.Models.ViewModels
{
    public class FilmGenreCountryDirectorRestrictionListViewModel
    {
        public List<Film> films { get; set; } = new();
        public List<AgeRestriction> restrictions { get; set; } = new();
        public List<Genre> genres { get; set; } = new();
        public List<OriginCountry> countries { get; set; } = new();
        public List<Director> directors { get; set; } = new();
        public List<FilmGenre> filmGenres { get; set; } = new();
        public List<FilmCountry> filmCountries { get; set; } = new();
    }
}
