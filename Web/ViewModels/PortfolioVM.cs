using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class PortfolioVM
    {
        public Guid Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
