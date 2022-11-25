using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redemption.Domain;
using Redemption.Interfaces;

namespace Redemption.Controllers
{
    [Authorize]
    public class RedemptionController : Controller
    {
        private readonly IRedemptionService _redemptionService;

        public RedemptionController(IRedemptionService redemptionService)
        {
            _redemptionService = redemptionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Company"] = new List<string>() {"RPS1","RPS2","Jassefy" };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRedemption model)
        {
            if (ModelState.IsValid)
            {
                var result = await _redemptionService.CreateAsync(model);
            }
            ModelState.AddModelError(string.Empty, "Не все поля введены");
            return RedirectToAction("Index", "Redemption");
        }
    }
}
