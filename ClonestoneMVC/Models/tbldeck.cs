//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClonestoneMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbldeck
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbldeck()
        {
            this.tbldeckcards = new HashSet<tbldeckcard>();
        }
    
        public int iddeck { get; set; }
        public string deckname { get; set; }
        public Nullable<int> fkperson { get; set; }
    
        public virtual tblperson tblperson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbldeckcard> tbldeckcards { get; set; }
    }
}
