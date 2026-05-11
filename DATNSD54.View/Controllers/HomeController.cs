using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;
using DATNSD54.View.IService;
using DATNSD54.View.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DATNSD54.View.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task< IActionResult> Index()
        {
            var products = await _homeService.GetAllProducts();
            if (products == null) { products = new List<ProductDisplayDTO>(); } ;

            return View(products);
        }

        public IActionResult Search( string? textSearch)
        {
            var products = _homeService.SearchProducts(textSearch).Result;
            if (products == null) { products = new List<ProductDTO>(); };

            return View(products);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
