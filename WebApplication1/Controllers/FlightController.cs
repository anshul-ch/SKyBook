using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            var criteria = new SearchCriteria
            {
                TravelDate = DateTime.Today.AddDays(1),
                NumberOfPassengers = 1,
                ClassType = "All"
            };

            ViewBag.Cities = _flightService.GetAllCities();
            return View(criteria);
        }

        [HttpPost]
        public IActionResult SearchResults(SearchCriteria searchCriteria)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = _flightService.GetAllCities();
                return View("Search", searchCriteria);
            }

            var flights = _flightService.SearchFlights(searchCriteria);

            var result = new FlightSearchResult
            {
                SearchCriteria = searchCriteria,
                Flights = flights,
                TotalResults = flights.Count
            };

            return View(result);
        }

        public IActionResult Details(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
                return NotFound();

            return View(flight);
        }

        [HttpPost]
        public IActionResult SortResults(SearchCriteria searchCriteria, string sortBy)
        {
            var flights = _flightService.SearchFlights(searchCriteria);

            if (sortBy == "Price")
                flights = _flightService.SortByPrice(flights);
            else if (sortBy == "Duration")
                flights = _flightService.SortByDuration(flights);

            var result = new FlightSearchResult
            {
                SearchCriteria = searchCriteria,
                Flights = flights,
                TotalResults = flights.Count
            };

            return View("SearchResults", result);
        }
    }
}
