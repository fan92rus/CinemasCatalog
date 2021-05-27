using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Models
{
    public class CinemaEditModel : CinemaAddModel
    {
        public int Id { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Постер")]
        public string Poster { get; set; }
    }
    public class CinemaAddModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Режисер")]
        public string Producer { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата премьеры")]
        public DateTime ReleaseTime { get; set; }

        [Display(Name = "Постер")]
        [DataType(DataType.Upload)]
        public IFormFile PosterFile { get; set; }
    }
    public class CinemaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Режисер")]
        public string Producer { get; set; }

        [Display(Name = "Дата релиза")]
        [DataType(DataType.Date)]
        public DateTime ReleaseTime { get; set; }

        [Display(Name = "Постер")]
        [DataType(DataType.ImageUrl)]
        public string Poster { get; set; }
        public string userId { get; set; }
    }

    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string Poster { get; set; }
        public User Author { get; set; }
        [NotMapped]
        public string userId => Author?.Id ?? null;
    }
}
