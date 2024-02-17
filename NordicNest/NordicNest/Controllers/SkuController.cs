using Microsoft.AspNetCore.Mvc;
using NordicNest.Model;
using NordicNest.Services;

namespace NordicNest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkuController : Controller
    {
        private readonly IPriceDetailService _priceDetailService;

        public SkuController(IPriceDetailService priceDetailService)
        {
            _priceDetailService = priceDetailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<PriceDetail>> GetPriceDetails(string id)
        {
            // TODO: Viewmodel for aggregating the data more nicely. Automapper?
            return _priceDetailService.GetPriceDetails(id)
                .ToList();
        }
    }
}
