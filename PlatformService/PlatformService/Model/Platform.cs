using System.ComponentModel.DataAnnotations;

namespace PlatformService.Model
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publlisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}