using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int pageIndex = 1,int pageSize=2)
        {
            if(pageIndex < 1)
            {
                pageIndex = 1;
            }
            var pagedTeams = await _service.GetPagedTeamsAsync(pageIndex, pageSize);
            return View(pagedTeams);
        }

        public IActionResult Create()
        {

           return View(); 
        
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team) 
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _service.AddTeam(team);

            }
            catch (FileRequiredException ex)
            {

                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {

                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {

                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existTeam = _service.GetTeam(x => x.Id == id);
            if (existTeam == null)  return NotFound();


            return View(existTeam);

        }
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                 _service.UpdateTeam(team);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (EntityFileNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {

                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {

                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id) 
        {
            try
            {
                _service.DeleteTeam(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (EntityFileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
