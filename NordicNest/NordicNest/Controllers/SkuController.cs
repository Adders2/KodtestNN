using Microsoft.AspNetCore.Mvc;
using NordicNest.Model;
using NordicNest.Model.DTO;
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
        public ActionResult<IEnumerable<PriceDetailDTO>> GetPriceDetails(string id)
        {
            return _priceDetailService.GetPriceDetails(id)
                .ToList();
        }
    }
}
