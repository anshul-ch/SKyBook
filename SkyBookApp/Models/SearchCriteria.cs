using System.ComponentModel.DataAnnotations;

namespace SkyBookApp.Models
{
    public class SearchCriteria
    {
        [Required(ErrorMessage = "Please select a departure city")]
        public string DepartureCity { get; set; }

        [Required(ErrorMessage = "Please select an arrival city")]
        public string ArrivalCity { get; set; }

        [Required(ErrorMessage = "Please select a travel date")]
        [DataType(DataType.Date)]
        public DateTime TravelDate { get; set; }

        [Required(ErrorMessage = "Please enter number of passengers")]
        [Range(1, 10, ErrorMessage = "Passengers must be between 1 and 10")]
        public int NumberOfPassengers { get; set; }

        public string ClassType { get; set; } = "All";
    }
}
