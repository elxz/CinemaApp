namespace Cinema.Models.ViewModels
{
    public class FilmGenreCountryDirectorRestrictionCheckBoxViewModel
    {
        public Film film { get; set; } = new();
        public Genre genre { get; set; } = new();
        public OriginCountry country { get; set; } = new();
        public Director director { get; set; } = new();
        public AgeRestriction restriction { get; set; } = new();
        public List<GenreCheckBoxViewModel> genreCheckBoxViewModel { get; set; } = new();
        public List<CountryCheckBoxViewModel> countryCheckBoxViewModel { get; set; } = new();
    }
}
