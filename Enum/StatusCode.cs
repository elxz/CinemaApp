namespace Cinema.Enum
{
    public enum StatusCode
    {
        FilmNotFound = 101,
        DirectorNotFound = 102,
        UserNotFound = 103,
        SessionNotFound = 104,
        TicketNotFound = 105,
        GenreNotFound = 106,
        CountryNotFound = 107,
        HallNotFound = 108,
        RestrictionNotFound = 109,
        FilmGenreNotFound = 110,
        FilmCountryNotFound = 111,
        SessionRowPlaceNotFound = 112,
        PricingNotFound = 113,
        CustomerTicketNotFound = 113,

        OK = 200,
        Error = 300,

        InternalServerError = 500
    }
}
