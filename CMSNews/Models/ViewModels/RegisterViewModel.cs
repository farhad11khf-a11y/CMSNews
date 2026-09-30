using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفاً نام را وارد کنید")]
        [MaxLength(30)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفاً شماره موبایل را وارد کنید")]
        [MaxLength(15)]
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفاً تکرار رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور صحیح نیست")]
        [Display(Name = "تکرار رمز عبور")]
        public string ConfirmPassword { get; set; }
    }
}