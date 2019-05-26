using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Models
{
    public class BaseTransactionModel : BaseModel
    {
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Hesap")]
        public int AccountId { get; set; }
        public SelectList AccountList { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public SelectList CategoryList { get; set; }

        [Display(Name = "Gelir Mi?")]
        public bool IsIncome { get; set; }

        //public List<QuickAddProperties> QuickAddList { get; set; }

        //public class QuickAddProperties
        //{
        //    public decimal Amount { get; set; }
        //    public string Description { get; set; }
        //    public int AccountId { get; set; }
        //    public int CategoryId { get; set; }
        //}

        public List<TransactionSummary> TransactionSummaries { get; set; }

        public class TransactionSummary
        {
            public int TransactionId { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Account { get; set; }
        }
    }
}