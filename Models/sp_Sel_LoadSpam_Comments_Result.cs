//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC.Models
{
    using System;
    
    public partial class sp_Sel_LoadSpam_Comments_Result
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string PostDate { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<long> IdArticle { get; set; }
        public Nullable<long> IdVideo { get; set; }
        public string IpComment { get; set; }
        public Nullable<int> Daduyet { get; set; }
        public string WebSite { get; set; }
        public string Images { get; set; }
        public Nullable<int> Spam { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
    }
}
