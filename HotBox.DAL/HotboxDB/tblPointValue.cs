//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotBox.DAL.HotboxDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPointValue
    {
        public int theIndex { get; set; }
        public System.DateTime DataTime { get; set; }
        public double DataValue { get; set; }
        public Nullable<int> Counter { get; set; }
    
        public virtual tblStrategy tblStrategy { get; set; }
    }
}
