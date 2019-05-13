﻿using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class CategoryConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
