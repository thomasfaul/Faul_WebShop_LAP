//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pack
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pack()
        {
            this.Order = new HashSet<Purchase>();
            this.AllDiscounts = new HashSet<Discount>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> CardQuantity { get; set; }
        public byte[] Image { get; set; }
        public string FlavorText { get; set; }
        public Nullable<bool> IsMoney { get; set; }
        public Nullable<int> NumberOfCards { get; set; }
        public Nullable<int> Worth { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ImageMimeType { get; set; }
        public Nullable<int> BasePrice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount> AllDiscounts { get; set; }
    }
}
