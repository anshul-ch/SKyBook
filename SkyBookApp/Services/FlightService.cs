using SkyBookApp.Models;

namespace SkyBookApp.Services
{
    public class FlightService
    {
        private readonly List<Flight> _flights = [];

        public FlightService()
        {
            SeedFlights();
        }

        public List<Flight> SearchFlights(SearchCriteria criteria)
        {
            var results = new List<Flight>();

            foreach (var f in _flights)
            {
                bool cityMatch = f.DepartureCity.Equals(criteria.DepartureCity, StringComparison.OrdinalIgnoreCase)
                              && f.ArrivalCity.Equals(criteria.ArrivalCity, StringComparison.OrdinalIgnoreCase);

                bool dateMatch = f.DepartureDateTime.Date == criteria.TravelDate.Date;
                bool seatsOk = f.AvailableSeats >= criteria.NumberOfPassengers;

                bool classMatch = string.IsNullOrEmpty(criteria.ClassType)
                               || criteria.ClassType.Equals("All", StringComparison.OrdinalIgnoreCase)
                               || f.ClassType.Equals(criteria.ClassType, StringComparison.OrdinalIgnoreCase);

                if (cityMatch && dateMatch && seatsOk && classMatch)
                    results.Add(f);
            }

            return results;
        }

        public List<string> GetAllCities()
        {
            var cities = new HashSet<string>();
            foreach (var f in _flights)
            {
                cities.Add(f.DepartureCity);
                cities.Add(f.ArrivalCity);
            }
            var list = cities.ToList();
            list.Sort();
            return list;
        }

        public Flight? GetFlightById(int id)
        {
            return _flights.Find(f => f.FlightId == id);
        }

        public List<Flight> SortByPrice(List<Flight> flights)
        {
            var sorted = new List<Flight>(flights);
            sorted.Sort((a, b) => a.Price.CompareTo(b.Price));
            return sorted;
        }

        public List<Flight> SortByDuration(List<Flight> flights)
        {
            var sorted = new List<Flight>(flights);
            sorted.Sort((a, b) => a.DurationInHours.CompareTo(b.DurationInHours));
            return sorted;
        }

        private void SeedFlights()
        {
            int id = 0;
            var tomorrow = DateTime.Today.AddDays(1);

            void Add(string number, string airline, string from, string to,
                     int depHour, int depMin, int arrHour, int arrMin,
                     decimal price, int seats, string cls)
            {
                _flights.Add(new Flight
                {
                    FlightId = ++id,
                    FlightNumber = number,
                    AirlineName = airline,
                    DepartureCity = from,
                    ArrivalCity = to,
                    DepartureDateTime = tomorrow.AddHours(depHour).AddMinutes(depMin),
                    ArrivalDateTime = tomorrow.AddHours(arrHour).AddMinutes(arrMin),
                    Price = price,
                    AvailableSeats = seats,
                    ClassType = cls
                });
            }

            // Delhi <-> Mumbai
            Add("AI101", "Air India",  "Delhi",     "Mumbai",     6,  0,  8, 30,  4500, 50, "Economy");
            Add("6E202", "IndiGo",     "Delhi",     "Mumbai",     9,  0, 11, 30,  3800, 30, "Economy");
            Add("SG303", "SpiceJet",   "Delhi",     "Mumbai",    14,  0, 16, 30,  3500, 45, "Economy");
            Add("AI501", "Air India",  "Delhi",     "Mumbai",    10,  0, 12, 30, 12000, 20, "Business");
            Add("6E502", "IndiGo",     "Delhi",     "Mumbai",    18,  0, 20, 30, 11500, 18, "Business");
            Add("AI701", "Air India",  "Delhi",     "Mumbai",    16,  0, 18, 30, 18000, 10, "First Class");

            Add("AI104", "Air India",  "Mumbai",    "Delhi",      7,  0,  9, 30,  4600, 40, "Economy");
            Add("6E205", "IndiGo",     "Mumbai",    "Delhi",     12,  0, 14, 30,  3900, 35, "Economy");
            Add("SG306", "SpiceJet",   "Mumbai",    "Delhi",     15,  0, 17, 30,  3700, 42, "Economy");
            Add("AI504", "Air India",  "Mumbai",    "Delhi",     11,  0, 13, 30, 12200, 22, "Business");
            Add("AI704", "Air India",  "Mumbai",    "Delhi",     19,  0, 21, 30, 18500, 12, "First Class");

            // Delhi <-> Bangalore
            Add("AI201", "Air India",  "Delhi",     "Bangalore",  8,  0, 11,  0,  5500, 55, "Economy");
            Add("6E301", "IndiGo",     "Delhi",     "Bangalore", 15,  0, 18,  0,  4800, 42, "Economy");
            Add("SG401", "SpiceJet",   "Delhi",     "Bangalore", 12,  0, 15,  0,  4600, 38, "Economy");
            Add("AI601", "Air India",  "Delhi",     "Bangalore", 13,  0, 16,  0, 13500, 24, "Business");
            Add("6E603", "IndiGo",     "Delhi",     "Bangalore", 17,  0, 20,  0, 12800, 20, "Business");
            Add("AI801", "Air India",  "Delhi",     "Bangalore", 20,  0, 23,  0, 19500,  8, "First Class");

            Add("AI204", "Air India",  "Bangalore", "Delhi",      6, 30,  9, 30,  5600, 48, "Economy");
            Add("6E304", "IndiGo",     "Bangalore", "Delhi",     11,  0, 14,  0,  4900, 45, "Economy");
            Add("SG404", "SpiceJet",   "Bangalore", "Delhi",     13,  0, 16,  0,  4700, 38, "Economy");
            Add("AI604", "Air India",  "Bangalore", "Delhi",     14,  0, 17,  0, 13700, 26, "Business");
            Add("AI804", "Air India",  "Bangalore", "Delhi",     21,  0, 24,  0, 19800, 10, "First Class");

            // Mumbai <-> Bangalore
            Add("AI301", "Air India",  "Mumbai",    "Bangalore",  7, 30,  9, 30,  4200, 52, "Economy");
            Add("6E402", "IndiGo",     "Mumbai",    "Bangalore", 13,  0, 15,  0,  3900, 44, "Economy");
            Add("SG501", "SpiceJet",   "Mumbai",    "Bangalore", 16,  0, 18,  0,  3700, 40, "Economy");
            Add("6E601", "IndiGo",     "Mumbai",    "Bangalore", 11,  0, 13,  0,  8500, 25, "Business");
            Add("AI651", "Air India",  "Mumbai",    "Bangalore", 19,  0, 21,  0,  8800, 22, "Business");
            Add("AI851", "Air India",  "Mumbai",    "Bangalore", 22,  0, 24,  0, 15500, 15, "First Class");

            Add("AI304", "Air India",  "Bangalore", "Mumbai",     8,  0, 10,  0,  4300, 50, "Economy");
            Add("6E405", "IndiGo",     "Bangalore", "Mumbai",    14,  0, 16,  0,  4000, 46, "Economy");
            Add("SG504", "SpiceJet",   "Bangalore", "Mumbai",    17,  0, 19,  0,  3800, 41, "Economy");
            Add("6E604", "IndiGo",     "Bangalore", "Mumbai",    12,  0, 14,  0,  8700, 24, "Business");
            Add("AI654", "Air India",  "Bangalore", "Mumbai",    20,  0, 22,  0,  9000, 20, "Business");
            Add("AI854", "Air India",  "Bangalore", "Mumbai",    23,  0, 25,  0, 15800, 14, "First Class");
        }
    }
}
