﻿using ExpenseTracker.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Models
{
    public class BaseTransactionModel : BaseModel
    {
        public List<TransactionSummary> TransactionSummaries { get; set; }

        public class TransactionSummary
        {
            public int TransactionId { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public int CategoryId { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public int AccountId { get; set; }
            public string Account { get; set; }
        }
    }

    public class BaseEditableTransactionModel : BaseTransactionModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public int AccountId { get; set; }
        public SelectList AccountList { get; set; }

        public int CategoryId { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

        public bool IsIncome { get; set; }
    }
}