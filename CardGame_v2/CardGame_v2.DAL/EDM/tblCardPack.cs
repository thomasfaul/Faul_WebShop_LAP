//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardGame_v2.DAL.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCardPack
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCardPack()
        {
            this.tblVirtualPurchase = new HashSet<tblVirtualPurchase>();
        }
    
        public int idCardPack { get; set; }
        public string packname { get; set; }
        public int numcards { get; set; }
        public int packprice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVirtualPurchase> tblVirtualPurchase { get; set; }
    }
}
