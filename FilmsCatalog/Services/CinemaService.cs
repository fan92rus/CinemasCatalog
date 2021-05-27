using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmsCatalog.Data;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace FilmsCatalog.Services
{
    public class CinemaService
    {
        public CinemaService(ApplicationDbContext context, ILogger<CinemaService> logger, IMapper mapper)
        {
            this.Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        public ApplicationDbContext Context { get; set; }
        public ILogger<CinemaService> Logger { get; }
        public IMapper Mapper { get; }

        public CinemaEditModel GetEditModel(CinemaViewModel model)
        {
            try
            {
                return Mapper.Map<CinemaEditModel>(model);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                throw e;
            }
        }
        public OperationResult Update(int cinemaId, CinemaAddModel newModel, User user)
        {
            try
            {
                var cinema = Context.Cinemas.FirstOrDefault(x => x.Id == cinemaId);
                if (cinema == null)
                    return new OperationResult(false, "Фильм не существует");

                if (user != null && !user.Equals(cinema.Author))
                    return new OperationResult(false, "Пользователь не может редактировать чужие фильмы");

                Context.Cinemas.Attach(cinema);

                var isLoadedPoster = FileUtilities.TryUploadedFile(newModel.PosterFile, "Images", out var posterPath);

                cinema.Description = newModel.Description;
                cinema.Producer = newModel.Producer;
                cinema.ReleaseTime = newModel.ReleaseTime;
                cinema.Title = newModel.Title;

                if (isLoadedPoster)
                    cinema.Poster = posterPath;

                Context.SaveChanges();

                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Create(CinemaAddModel model, User user)
        {
            try
            {
                var isLoadedPoster = FileUtilities.TryUploadedFile(model.PosterFile, "Images", out var posterPath);

                var cinema = new Cinema()
                {
                    Author = user,
                    Description = model.Description,
                    Producer = model.Producer,
                    ReleaseTime = model.ReleaseTime,
                    Title = model.Title
                };

                if (isLoadedPoster)
                    cinema.Poster = posterPath;

                Context.Cinemas.Add(cinema);
                var changes = Context.SaveChanges();

                return new OperationResult(changes > 0);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                return new OperationResult(false, e.Message);
            }
        }
        public async Task<OperationResult<CinemaViewModel>> GetOne(int id)
        {
            try
            {
                return new(true, Mapper.Map<CinemaViewModel>(Context.Cinemas.FirstOrDefault(x => x.Id == id)), "");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                return new(false, null, e.Message);
            }
        }
        public async Task<OperationResult<PagedResult<CinemaViewModel>>> Get(int page, int size, User user)
        {
            try
            {
                var cinemas = await Context.Cinemas.Select(x => Mapper.Map<CinemaViewModel>(x))
                    .GetPagedResultAsync(page, size);

                return new(true, cinemas, "");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "");
                return new(false, null, e.Message);
            }
        }
    }
}
