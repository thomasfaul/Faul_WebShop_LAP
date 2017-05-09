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
    
    public partial class Purchase
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> OrderDateTime { get; set; }
        public Nullable<int> ID_User { get; set; }
        public Nullable<int> ID_CardPack { get; set; }
        public Nullable<int> TotalCost { get; set; }
        public Nullable<int> NumberOfPackagesBought { get; set; }
        public string KindOfPayment { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Pack CardPack { get; set; }
        public virtual User User { get; set; }
    }
}
