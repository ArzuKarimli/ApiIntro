using System.ComponentModel.DataAnnotations;

namespace ApiIntro.Models
{
    public class City: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
