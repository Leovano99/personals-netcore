using System.ComponentModel.DataAnnotations;

namespace VDI.Demo.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
