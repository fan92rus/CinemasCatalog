using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FilmsCatalog.Data;
using FilmsCatalog.Models;
using FilmsCatalog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;

namespace FilmsCatalog.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> UserManager { get; }

        public UserController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        protected async Task<User> GetUser()
        {
            if (User.Identity.IsAuthenticated)
                return await UserManager.GetUserAsync(User);

            return null;
        }
    }
    public class CinemaController : UserController
    {
        public CinemaService CinemaService { get; }
        public ILogger<CinemaController> Logger { get; }

        public CinemaController(CinemaService cinemaService, UserManager<User> manager, ILogger<CinemaController> logger) : base(manager)
        {
            CinemaService = cinemaService;
            Logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cinema = await CinemaService.GetOne(id);
                return View(CinemaService.GetEditModel(cinema.Value));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var cinema = await CinemaService.GetOne(id);
                if (cinema.Succeeded)
                    return View(cinema.Value);
                else
                    return RedirectToAction("Get");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CinemaEditModel model)
        {
            try
            {
                var cinema = await CinemaService.GetOne(model.Id);

                if (!ModelState.IsValid)
                    return View(CinemaService.GetEditModel(cinema.Value));
                var user = await GetUser();
                var res = CinemaService.Update(model.Id, model, user);

                if (!res.Succeeded)
                    return View(CinemaService.GetEditModel(cinema.Value));

                return RedirectToAction("Edit", new { id = model.Id });
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CinemaAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var user = await GetUser();
                var res = CinemaService.Create(model, user);

                return RedirectToAction("Get");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] Pagination offset)
        {
            try
            {
                var res = await CinemaService.Get(offset.Page, offset.Size, await this.GetUser());

                return View(res.Value ?? new PagedResult<CinemaViewModel>());
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw;
            }
        }
    }
}
