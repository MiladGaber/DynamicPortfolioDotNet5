using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class PortfolioItem : EntityBase
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
    }
}