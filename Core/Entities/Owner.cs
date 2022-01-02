using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Owner : EntityBase
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string JopTitle { get; set; }
        [Required]
        public string AvatarUrl { get; set; }

        public Address Address{ get; set; }
    }
}
