using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionTitle { get; set; }
        public Category? Category { get; set; }
        public int Amount { get; set; }
        public string TransactionDescription { get; set; }
    }
}
