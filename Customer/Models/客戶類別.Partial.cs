namespace Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶類別MetaData))]
    public partial class 客戶類別
    {
    }
    
    public partial class 客戶類別MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 類別名稱 { get; set; }
    }
}
