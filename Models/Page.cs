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
    using System.Collections.Generic;
    
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            this.Articles = new HashSet<Article>();
        }
    
        public int Id { get; set; }
        public Nullable<int> MenuId { get; set; }
        public string Category { get; set; }
        public string Discription { get; set; }
        public string PageUrl { get; set; }
        public Nullable<int> position { get; set; }
        public string LinkUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
