using System.ComponentModel.DataAnnotations;

namespace Chess.API.DTO.Input
{
    public class ValueDto
    {
        [Required]
        public string Value { get; set; }
    }
}