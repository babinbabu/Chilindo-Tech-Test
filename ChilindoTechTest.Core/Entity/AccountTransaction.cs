//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChilindoTechTest.Core.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountTransaction
    {
        public int TransactionId { get; set; }
        public int AccountNumber { get; set; }
        public Common.Enums.ChilindoCurrencyType CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public Common.Enums.ChilindoTransactionType TransactionType { get; set; }
        public System.DateTime TransactionDate { get; set; }
    
        public virtual AccountDetail AccountDetail { get; set; }
    }
}
