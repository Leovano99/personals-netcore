using System.ComponentModel.DataAnnotations;

namespace VDI.Demo.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}