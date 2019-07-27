using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

// B1 đặt tên namespace cùng với namespace trong file Article.cs của DbModel.tt
namespace MVC.Models
{
    [MetadataType(typeof(ArticleMetaData))]

    // B2 đặt partial cho class
    public partial class Article
    {
    }

    // B3 tạo class mới ArticleMetaData sử lý các lỗi validation, thay vì sử lý trong file Article.cs của Db.Model.tt
    // vì khi cập nhật Entity framework sẽ bị mất hết validation
    public class ArticleMetaData
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string body { get; set; }
        
    }
}