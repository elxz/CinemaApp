using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Cinema.Models.ViewModels
{
    public class FilmRestrictionDirectorGenreCountryViewModel
    {
        public Film film { get; set; } = new();
        public AgeRestriction restriction { get; set; } = new();
        public Director director { get; set; } = new();
        public Genre genre { get; set; } = new();
        public OriginCountry country { get; set; } = new();
    }
}
