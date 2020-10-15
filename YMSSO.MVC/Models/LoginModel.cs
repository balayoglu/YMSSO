using System.ComponentModel.DataAnnotations;

namespace YMDiscourseSSO.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Redirect { get; set; }
    }
}
