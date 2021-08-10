using System.ComponentModel.DataAnnotations;

namespace ner_pr_api.Dtos.InputDtos
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}