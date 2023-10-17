using Cinema.Service.Interfaces;

namespace Cinema.Models.ViewModels
{
    public class GenreCheckBoxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
