using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.Domain.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [EmailAddress(ErrorMessage = "قالب {0} اشتباه است")]
        public string Email { get; set; }
    }
}
