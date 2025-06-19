using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestApp.Domain;

namespace MyTestApp.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public partial class AdminContriller : Controller
    {
        private readonly DataManager _dataManager;

        public AdminContriller(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View();

        }





    }
}
