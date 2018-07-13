namespace Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public const string Mobile = "\\d{4}-\\d{6}";
        //IUnitOfWork EF = RepositoryHelper.GetUnitOfWork();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var customerContactPerson = RepositoryHelper.Get客戶聯絡人Repository();
            var result = customerContactPerson.Query(x => x.Email.Equals(this.Email) && x.客戶Id == this.客戶Id).Count();

            if (result >= 1)
            {
                yield return new ValidationResult("EMail 已存在，請提供另一組 EMail。", new string[] { "Email" });
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }

        [Mobile]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }

    /// <summary>
    /// TaiwanIDCardNumber Property Valid
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MobileAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            string result = $"{name} 格式必須為 09xx - xxxxx";
            return result;
        }

        public override bool IsValid(object value)
        {
            bool result = false;

            if (value != null)
            {
                result = Regex.IsMatch((string)value, 客戶聯絡人.Mobile);
            }

            return result;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value) == true)
            {
                return ValidationResult.Success;
            }
            else
            {
                List<string> Members = new List<string>();
                Members.Add(validationContext.MemberName);
                return new ValidationResult(FormatErrorMessage(validationContext.MemberName), Members.AsEnumerable());
            }
        }
    }
}
