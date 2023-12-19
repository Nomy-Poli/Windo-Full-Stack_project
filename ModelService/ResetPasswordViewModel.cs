using System;
using System.ComponentModel.DataAnnotations;

namespace ModelService
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "הכנס בבקשה כתובת מייל תקינה")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$", ErrorMessage = "הסיסמה החדשה אינה תקינה")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "סיסמת האימות אינה תואמת לסיסמה החדשה.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
