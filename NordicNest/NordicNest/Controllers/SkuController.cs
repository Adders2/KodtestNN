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
        public ActionResult<IEnumerable<PriceDetail>> GetAll()
        {
            return _priceDetailService.GetPriceDetails().ToList();
        }
    }
}
