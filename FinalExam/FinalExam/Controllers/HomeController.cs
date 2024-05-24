
using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _service;

        public HomeController(ITeamService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var teams = _service.GetAllTeams();
            return View(teams);
        }

       
    }
}
