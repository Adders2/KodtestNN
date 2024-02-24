using Microsoft.AspNetCore.Mvc;
using NordicNest.Controllers.VM;
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
        public ActionResult<IEnumerable<PriceDetailsViewModel>> GetPriceDetails(string id)
        {
            var priceDetails = _priceDetailService.GetPriceDetails(id);

            var vm = priceDetails.Select(pd => new PriceDetailsViewModel(pd)).ToList();

            return Ok(vm);
        }
    }
}
