namespace WebApplication1.Models
{
    public class FlightSearchResult
    {
        public SearchCriteria SearchCriteria { get; set; }
        public List<Flight> Flights { get; set; } = [];
        public int TotalResults { get; set; }
    }
}
