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
    
    public partial class sp_sel_Article_Lienquan_Result
    {
        public long Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string body { get; set; }
        public string Source { get; set; }
        public string CreateDate { get; set; }
        public string LastModified { get; set; }
        public bool Show { get; set; }
        public bool isHot { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public string FilePath { get; set; }
        public Nullable<long> ReadNumber { get; set; }
        public Nullable<long> VideoId { get; set; }
        public string TitleUrl { get; set; }
        public string ListArticleId { get; set; }
    }
}
