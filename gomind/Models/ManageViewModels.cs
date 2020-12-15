using gomind.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{
    public class Manage
    {
        public IndexViewModel IndexViewModel { get; set; }
        public ManageLoginsViewModel ManageLoginsViewModel { get; set; }
        public FactorViewModel FactorViewModel { get; set; }
        public SetPasswordViewModel SetPasswordViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public AddPhoneNumberViewModel AddPhoneNumberViewModel { get; set; }
        public VerifyPhoneNumberViewModel VerifyPhoneNumberViewModel { get; set; }
        public ConfigureTwoFactorViewModel ConfigureTwoFactorViewModel { get; set; }
        public IEnumerable<UserData> UserData { get; set; }
        public IEnumerable<SaleSetting> SaleSetting { get; set; }
        public IEnumerable<FollowTable> FollowTable { get; set; }
        public IEnumerable<FollowUser> FollowUser { get; set; }
        public IEnumerable<ProductList> ProductList { get; set; }
        public IEnumerable<Setting> Setting { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public List<Order> BuyOrder { get; set; }
        public IEnumerable<Chat> Chat { get; set; }
        public IEnumerable<Comment_Member> GiveComment { get; set; }
        public IEnumerable<Comment_Member> GetComment { get; set; }
        public int number { get; set; }
        public int talk { get; set; }
    }
    public class ChatIndex
    {
        public Chat Chat { get; set; }
        public IEnumerable<ChatMessage> ChatMessage { get; set; }
        public ChatMessage Message { get; set; }
        public bool IsOrder { get; set; }
        public bool IsSale { get; set; }
        public bool IsUser { get; set; }
        public bool ImageUrl { get; set; }
    }
    public class sale
    {
        public bool IsUser { get; set; }
        public IEnumerable<ProductList> ProductList { get; set; }
        public List<Comment_Member> Comment_Member { get; set; }
        public IEnumerable<UserData> UserData { get; set; }
        public IEnumerable<UserFile> UserFile { get; set; }
        public bool Follow { get; set; }
        public List<One> one { get; set; }
        public Two two { get; set; }
        public string id { get; set; }
        public bool img {get;set;}
        public string star { get; set; }
        public string ones { get; set; }
        public string twos { get; set; }
        public string three { get; set; }
        public string four { get; set; }
        public string five { get; set; }
        public int count { get; set; }
        public int? foll { get; set; }
        public IEnumerable<kind> kind { get; set; }
    }
    public class ProductListSearch
    {
        public IEnumerable<ProductList> ProductList { get; set; }

    }
    public class kind {
        public List<One> one { get; set; }
        public List<One> oneall { get; set; }
        public string id { get; set; }
    }
    public class ProductListDetails
    {
        public ProductList ProductList { get; set; }
        public IEnumerable<Comment_Product> Comment_Product { get; set; }
        public Comment_Product Create_Comment { get; set; }
        public bool Follow { get; set; }
        public bool isUser { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<SaleSetting> SaleSetting { get; set; }
        public IEnumerable<File> File { get; set; }
        public bool IsOrder { get; set; }
        public DateTime day { get; set; }
    }
    public class ProductListEdits
    {
        public ProductList ProductList { get; set; }
        public SaleSetting SaleSetting { get; set; }
        public File File { get; set; }
        public int o { get; set; }
        public int t { get; set; }
    }
    public class ProductListCreate
    {
        public ProductList ProductList { get; set; }
        public SaleSetting SaleSetting { get; set; }
    }

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
     
        public bool TwoFactor { get; set; }
        [DisplayName("帳號")]
        public string Accout { get; set; }
        public string id { get; set; }
        [StringLength(255)]
        [DisplayName("暱稱")]
        public string Nickname { get; set; }
        [DisplayName("生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime? Birthday { get; set; }
        [StringLength(255)]
        [DisplayName("性別")]
        public string Sex { get; set; }
     
        [DisplayName("加入時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime? addtime { get; set; }
        public string Text { get; set; }
        [DisplayName("大頭貼")]
        public bool ImageUrl { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("NewPassword", ErrorMessage = "確認密碼與新密碼不相符，請重新輸入")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "舊密碼")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("NewPassword", ErrorMessage = "確認密碼與新密碼不相符，請重新輸入")]
        public string ConfirmPassword { get; set; }
    }


    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "手機號碼")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "認證碼")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "手機號碼")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

}